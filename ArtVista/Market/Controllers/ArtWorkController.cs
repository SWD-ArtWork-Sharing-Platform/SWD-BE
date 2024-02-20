using Market.Models.DTO;
using Market.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Market.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtWorkController : ControllerBase
    {
        protected ResponseDTO _response;
        private IArtworkServices _artwork_service;
        public ArtWorkController(IArtworkServices u)
        {
            this._response = new ResponseDTO();
            _artwork_service = u;
        }

        //[Authorize(Policy = "MPVI_WH_Management")]
        // Book an Report Ticket
        [HttpGet("GetAllArtwork")]
        public async Task<ResponseDTO> GetAllArtwork(string? searchkey, decimal? minPrice,
            decimal? MaxPrice, decimal? discount, string? status, string? cateID)
        {
            try
            {
                _response.Result = await _artwork_service.GetAllArtwork
                        (searchkey,minPrice,MaxPrice,discount,status,cateID);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("CreateArtWork")]
        public async Task<ResponseDTO> CreateArtWork(ArtWorkDTO artworkDTO)
        {
            try
            {
                _response.IsSuccess = await _artwork_service.CreateArtWork(artworkDTO);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("UpdateArtWork")]
        public async Task<ResponseDTO> UpdateArtWork(ArtWorkDTO artworkDTO)
        {
            try
            {
                _response.IsSuccess = await _artwork_service.UpdateArtWork(artworkDTO);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("DeleteArtWork")]
        public async Task<ResponseDTO> DeleteArtWork(string id)
        {
            try
            {
                _response.IsSuccess = await _artwork_service.DeleteArtWork(id);
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
