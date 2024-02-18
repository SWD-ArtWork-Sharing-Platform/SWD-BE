using Management.Models.DTO;
using Management.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace Management.Controllers
{
    [Route("api/artwork")]
    [ApiController]
    public class ArtworkController : ControllerBase
    {
        protected ResponseDTO _response;
        private IArtworkService _artworkService;

        public ArtworkController(IArtworkService artworkService)
        {
            this._response = new ResponseDTO();
            _artworkService = artworkService;       
        }

        [HttpGet("GettAllArtwork")]
        public async Task<ResponseDTO> GetAllArtwork()
        {
            try
            {
                _response.Result = await _artworkService.GetAllArtwork();   
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetAartworkByID")]
        public async Task<ResponseDTO> GetArtworkByID(string id)
        {
            try
            {
                _response.Result = await _artworkService.GetArtworkById(id);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("CreateNewArtwork")]
        public async Task<ResponseDTO> CreateNewArtwork(ArtworkDTO artworkDTO)
        {
            try
            {
                _response.Result = await _artworkService.CreateNewArtwork(artworkDTO);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("UpdateArtwork")]
        public async Task<ResponseDTO> UpdateArtwork(ArtworkDTO artworkDTO)
        {
            try
            {
                _response.Result = await _artworkService.UpdateArtwork(artworkDTO);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetArtWorkByCondition")]
        public async Task<ResponseDTO> GetArtWorkByCondition(string? name, string id, string status, decimal discount)
        {
            try
            {
                _response.Result = await _artworkService.GetArtWorkByCondition(name, id, status, discount);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeleteArtWorkByID")]
        public async Task<ResponseDTO> DeleteArtWorkByID(string id, bool confirm)
        {
            try
            {
                _response.Result = await _artworkService.DeleteArtWorkByID(id, confirm);
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
