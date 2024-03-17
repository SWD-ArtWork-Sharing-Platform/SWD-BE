using Management.Util;
using Market.Data;
using Market.Models;
using Market.Models.DTO;
using Market.Models.Payment.PaymentResponse;
using Market.Repository.IRepository;
using Market.Services;
using Market.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("api/packagePurchase")]
    [ApiController]
    public class PackagePurchaseController : ControllerBase
    {
        protected ResponseDTO _response;
        private IPackageServices _package_services;
        private IVnPayService _vnPayService;
        private readonly IConfiguration _configuration;
        private ArtworkSharingPlatformContext _db;
        private IBankAccountRepository _bankAccountRepository;

        public PackagePurchaseController(IPackageServices wishlist_servoces, IVnPayService vnPayService, ArtworkSharingPlatformContext db, IBankAccountRepository bankAccountRepository)
        {
            this._response = new ResponseDTO();
            _package_services = wishlist_servoces;
            _vnPayService = vnPayService;   
            _db = db;
            _bankAccountRepository = bankAccountRepository;
        }


        [HttpGet("GetAllAvailablePackage")]
        public async Task<ResponseDTO> GetAllAvailablePackage(string? name, int? max, decimal? price, decimal? discount)
        {
            try
            {
                _response.Result = await _package_services.GetAllAvailablePackage(name, max,  price,  discount);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize]
        [HttpGet("GetAllPurchasePackagebyUserID")]
        public async Task<ResponseDTO> GetAllPurchasePackagebyUserID(string userID)
        {
            try
            {
                _response.Result = await _package_services.GetAllPurchasePackagebyUserID( userID);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize]
        [HttpPost("BuyPackage")]
        public async Task<ResponseDTO> BuyPackage(string userID, PackageOFCreatorDTO obj)
        {
            try
            {
                _response.IsSuccess = await _package_services.BuyPackage( userID,  obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize(Policy = "ORGANIZATION")]
        [HttpPost("AdminUpdatePackage")]
        public async Task<ResponseDTO> AdminUpdatePackage(PackageDTO obj)
        {
            try
            {
                _response.IsSuccess = await _package_services.AdminUpdatePackage( obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize(Policy = "ORGANIZATION")]
        [HttpDelete("AdminDeletePackage")]
        public async Task<ResponseDTO> AdminDeletePackage(string packageID)
        {
            try
            {
                _response.IsSuccess = await _package_services.AdminDeletePackage( packageID);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize]
        [HttpPost("CreatePaymentPackage")]
        public IActionResult CreatePaymentUrl(PackageOFCreatorDTO model)
        {
            var url = _vnPayService.CreatePaymentUrlForPackage(model, HttpContext);
            if (!string.IsNullOrEmpty(url))
            {
                return Ok(new { Url = url });
            }
            else
            {
                return BadRequest("Failed to create payment URL");
            }
        }

        [HttpGet("PaymentCallBackPackage")]
        public async Task<IActionResult> PaymentCallBackPackage()
        {
            var urlCallBack = _configuration["PaymentCallBackForPackage:ReturnUrl"];
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
            if (string.IsNullOrEmpty(response.PaymentMethod) ||
              string.IsNullOrEmpty(response.OrderDescription) ||
              string.IsNullOrEmpty(response.Order_Id) ||
              string.IsNullOrEmpty(response.PaymentId) ||
              string.IsNullOrEmpty(response.TransactionId) ||
              string.IsNullOrEmpty(response.Token) ||
              string.IsNullOrEmpty(response.VnPayResponseCode))
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

            string packageId = Convert.ToString(orderParts[0]);
            double amount = Convert.ToDouble(orderParts[1]);
            string userId = Convert.ToString(orderParts[2]);

            var packageOfCreator = _db.DPackageOfCreators.FirstOrDefault(u => u.PackageId == packageId || u.Id == userId);
            if (packageOfCreator == null)
            {
                _response.Message = "There is no valid package's payment of customer with id: " + userId + "for order's id: " + packageId;
            }
            else 
            {
                if (paymentResult.VnPayResponseCode == "00")
                {

                    DPaymentResponse paymentResponse = new DPaymentResponse
                    {
                        OrderDescription = paymentResult.OrderDescription,
                        OrderId = paymentResult.Order_Id,
                        PayDate = DateTime.Now,
                        PaymentMethod = paymentResult.PaymentMethod,
                        Success = true,
                        Token = paymentResult.Token,
                        TransactionId = paymentResult.TransactionId,
                        VnPayResponseCode = paymentResult.VnPayResponseCode,
                        PaymentId = paymentResult.PaymentId,
                        Amount = amount,
                    };

                    await _db.DPaymentResponses.AddAsync(paymentResponse);      
                    await _db.SaveChangesAsync();   

                    packageOfCreator.Status = SD.PackageStatus.ACTIVE; 
                    packageOfCreator.GraceDate = DateTime.Now;  
                }
            }

            var vnPayResponse = _vnPayService.PaymentExecute(Request.Query);
            await _db.SaveChangesAsync();

            return Redirect(urlCallBack);
        }
    }
}
