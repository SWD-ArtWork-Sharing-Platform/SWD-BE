using Azure;
using Management.Util;
using Market.Data;
using Market.Models;
using Market.Models.DTO;
using Market.Models.Payment.PaymentResponse;
using Market.Repository;
using Market.Repository.IRepository;
using Market.Services;
using Market.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        protected ResponseDTO _response;
        private IOrderService _order_services;
        private IVnPayService _vnPayService;
        private readonly IConfiguration _configuration;
        private ArtworkSharingPlatformContext _db;
        private IBankAccountRepository _bankAccountRepository;
        public OrderController(IOrderService wishlist_servoces, IVnPayService vnPayService, IConfiguration configuration, ArtworkSharingPlatformContext db, IBankAccountRepository bankAccountRepository)
        {
            this._response = new ResponseDTO();
            _order_services = wishlist_servoces;
            _vnPayService = vnPayService;
            _configuration = configuration;
            _db = db;
            _bankAccountRepository = bankAccountRepository;
        }

        //[Authorize]
        [HttpGet("GetOrder")]
        public async Task<ResponseDTO> GetOrder(string orderID, string? status, DateTime? CreatedOn)
        {
            try
            {
                _response.Result = await _order_services.GetOrder(orderID, status, CreatedOn);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize]
        [HttpPut("DownloadArtWorkCheck")]
        public async Task<ResponseDTO> DownloadArtWorkCheck(string userID, string OrderID, string artID)
        {
            try
            {
                _response.IsSuccess = await _order_services.DownloadArtWorkCheck(userID, OrderID, artID);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        //[Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ResponseDTO> CreateOrder(OrderResponseDTO obj)
        {
            try
            {
                _response.IsSuccess = await _order_services.CreateOrder(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize]
        [HttpGet("GetHistoryOrder")]
        public async Task<ResponseDTO> GetHistoryOrder(string userID)
        {
            try
            {
                _response.Result = await _order_services.GetHistoryOrder(userID);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize]
        [HttpPost("CreatePaymentUrl")]
        public IActionResult CreatePaymentUrl(OrderDTO orderDTO)
        {
            var url = _vnPayService.CreatePaymentUrl(orderDTO, HttpContext);

            if (!string.IsNullOrEmpty(url))
            {
                return Ok(new { Url = url });
            }
            else
            {
                return BadRequest("Failed to create payment URL");
            }
        }


        [HttpGet("PaymentCallback")]
        public async Task<IActionResult> PaymentCallback()
        {


            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            var response = new PaymentResponse
            {
                OrderDescription = Request.Query["vnp_OrderInfo"],
                Order_Id = Request.Query["vnp_TxnRef"],
                PaymentId = Request.Query["vnp_TransactionNo"],
                TransactionId = Request.Query["vnp_TransactionNo"],
                Token = Request.Query["vnp_SecureHash"],
                VnPayResponseCode = Request.Query["vnp_ResponseCode"],
                Success = true,
            };

            if (
                string.IsNullOrEmpty(response.OrderDescription) ||
                string.IsNullOrEmpty(response.Order_Id) ||
                string.IsNullOrEmpty(response.PaymentId) ||
                string.IsNullOrEmpty(response.TransactionId) ||
                string.IsNullOrEmpty(response.Token) ||
                string.IsNullOrEmpty(response.VnPayResponseCode) 
                )
            {
                return BadRequest("Invalid payment data received");
            }

            var paymentResult = new PaymentResponse
            {
                OrderDescription = response.OrderDescription,
                Order_Id = response.Order_Id,
                PaymentId = response.PaymentId,
                TransactionId = response.TransactionId,
                Token = response.Token,
                VnPayResponseCode = response.VnPayResponseCode,
            };


            string[] orderParts = paymentResult.OrderDescription.Split(' ');

            string userId = Convert.ToString(orderParts[0]);
            double amount = Convert.ToDouble(orderParts[1]);
            string orderId = Convert.ToString(orderParts[2]);
            int numberOfDowload = Convert.ToInt32(orderParts[3]);

            var order = _db.FOrders.FirstOrDefault(u => u.Id == userId && u.OrderId == orderId);
            if (order == null)
            {
                _response.Message = "There is no valid order's payment of customer with id: " + userId + "for order's id: " + orderId;
            }

            if (order != null)
            {
                if (paymentResult.VnPayResponseCode == "00")
                {



                    DPaymentResponse paymentResponse = new DPaymentResponse
                    {
                        OrderDescription = paymentResult.OrderDescription,
                        OrderId = paymentResult.Order_Id,
                        PaymentId = paymentResult.PaymentId,
                        TransactionId = paymentResult.TransactionId,
                        Amount = amount,
                        PayDate = DateTime.Now,
                        Token = paymentResult.Token,
                        Success = true,
                        VnPayResponseCode = paymentResult.VnPayResponseCode,
                        PaymentMethod = paymentResult.PaymentMethod,
                    };

                    await _db.DPaymentResponses.AddAsync(paymentResponse);
                    await _db.SaveChangesAsync();

                    order.PaymentResponseId = paymentResponse.PaymentResponseId;    
                    order.OrderStatus = SD.OrderStatus.SUCCESS_PAY_VNPAY;
                    var userBank = _bankAccountRepository.Get(u => u.UserId == userId);
                    if (userBank != null)
                    {
                        userBank.Balance += order.Total;
                    }
                }
            }
            var vnPayResponse = _vnPayService.PaymentExecute(Request.Query);
            _response.Result = vnPayResponse;
            _db.SaveChanges();

            return Redirect(urlCallBack);
        }

    }
}