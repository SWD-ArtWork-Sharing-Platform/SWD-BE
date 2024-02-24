using Market.Models.DTO;
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
        public PackagePurchaseController(IPackageServices wishlist_servoces)
        {
            this._response = new ResponseDTO();
            _package_services = wishlist_servoces;
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

        [Authorize]
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

        [Authorize]
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

        [Authorize(Policy = "ORGANIZATION")]
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

        [Authorize(Policy = "ORGANIZATION")]
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

    }
}
