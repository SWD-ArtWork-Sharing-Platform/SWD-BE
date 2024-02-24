using Azure;
using Management.Models.DTO;
using Management.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Route("api/interaction")]
    [ApiController]
    
    public class InteractionController : ControllerBase
    {
        protected ResponseDTO _response;
        private IInteractionService _interactionService;

        public InteractionController(IInteractionService interactionService)
        {
            this._response = new ResponseDTO();
            _interactionService = interactionService;
        }

        [HttpGet("GetAllInteraction")]
        public ResponseDTO GetAllInteraction()
        {
            try
            {
                _response.Result = _interactionService.GetAllInteraction();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet("GetInteractionByID")]
        public ResponseDTO GetInteractionByID(string interactionID)
        {
            try
            {
                _response.Result = _interactionService.GetInteractionByID(interactionID);       
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Authorize]
        [HttpGet("GetInteractionByUserID")]
        public ResponseDTO GetInteractionByUserID(string userID)
        {
            try
            {
                _response.Result = _interactionService.GetInteractionByUserID(userID);      
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetInteractionByPostID")]
        public ResponseDTO GetInteractionByPostID(string postId)
        {
            try
            {
                _response.Result = _interactionService.GetInteractionByPostId(postId);      
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [Authorize]
        [HttpPost("CreateInteraction")]
        public ResponseDTO CreateInteraction(InteractionDTO interactionDTO)
        {
            try
            {
                _response.Result = _interactionService.CreateInteraction(interactionDTO);       
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [Authorize]
        [HttpPut("UpdateInteraction")]
        public ResponseDTO UpdateInteraction(InteractionDTO interactionDTO) 
        {
            try
            {
                _response.Result = _interactionService.UpdateInteraction(interactionDTO);   
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }



        [Authorize]
        [HttpDelete("DeleteInteraction")]
        public ResponseDTO DeleteInteraction(string id)
        {
            try
            {
                _response.Result = _interactionService.DeleteInteraction(id);       
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;   
        }
    }
}
