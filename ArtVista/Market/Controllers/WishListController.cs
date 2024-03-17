using Market.Models.DTO;
using Market.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("api/wishlist")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        protected ResponseDTO _response;
        private IWishListServices _wishlist_services;
        public WishListController(IWishListServices wishlist_servoces)
        {
            this._response = new ResponseDTO();
            _wishlist_services = wishlist_servoces;
        }

       // [Authorize]
        [HttpGet("GetWishList")]
        public async Task<ResponseDTO> GetWishList(string userID)
        {
            try
            {
                _response.Result = await _wishlist_services.GetWishList(userID);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize]
        [HttpDelete("RemoveArtWorkFromWishList")]
        public async Task<ResponseDTO> RemoveArtWorkFromWishList(string userID, string artworkID)
        {
            try
            {
                _response.IsSuccess = await _wishlist_services.RemoveArtWorkFromWishList(userID,artworkID);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize]
        [HttpPost("AddArtWorkToWishList")]
        public async Task<ResponseDTO> AddArtWorkToWishList(string userID, string artwork, int quantity)
        {
            try
            {
                _response.IsSuccess = await _wishlist_services.AddArtWorkToWishList( userID,  artwork, quantity);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize]
        [HttpPut("UpdateWishList")]
        public async Task<ResponseDTO> UpdateWishList(string userID, WishList obj)
        {
            try
            {
                _response.IsSuccess = await _wishlist_services.UpdateWishList( userID,  obj);
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
