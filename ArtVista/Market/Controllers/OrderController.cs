using Azure;
using Market.Models.DTO;
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
        public OrderController(IOrderService wishlist_servoces, IVnPayService vnPayService)
        {
            this._response = new ResponseDTO();
            _order_services = wishlist_servoces;
            _vnPayService = vnPayService;   
        }

        [Authorize]
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

        [Authorize]
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


        [Authorize]
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

        [Authorize]
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

        [HttpPost("PaymentCallback")]
        public ResponseDTO PaymentCallback()
        {
            try
            {
                var vnPayResponse = _vnPayService.PaymentExecute(Request.Query);
                _response.Result = vnPayResponse;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}