using Management.Models.DTO;
using Management.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private ResponseDTO _response;
        private IPostService _postService;

        public PostController(IPostService postService)
        {
            this._response = new ResponseDTO(); 
            _postService = postService; 
        }

        [Authorize(Policy = "ARTWORKMANAGEMENT")]
        [HttpPost("CreateNewPost")]
        public ResponseDTO CreateNewPost(PostDTO postDTO)
        {
            try
            {
                _response.Result = _postService.CreateNewPost(postDTO);     
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Authorize(Policy = "ARTWORKMANAGEMENT")]
        [HttpDelete("DeletePostByID")]
        public ResponseDTO DeletePostByID(string id)
        {
            try
            {
                _response.Result = _postService.DeletePostByID(id);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet("GetPostByCondition")]
        public ResponseDTO GetPostByCondition(string title, string artworkId)
        {
            try
            {
               _response.Result = _postService.GetPostByCondition(title, artworkId);       
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetPostByID")]
        public ResponseDTO GetPostByID(string id)
        {
            try
            {
                _response.Result = _postService.GetPostByID(id);        
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetAllPost")]
        public ResponseDTO GetAllPost()
        {
            try
            {
                _response.Result = _postService.GetAllPost();   
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Authorize(Policy = "ARTWORKMANAGEMENT")]
        [HttpPut("UpdatePost")]
        public ResponseDTO UpdatePost(PostDTO postDTO)
        {
            try
            {
                _response.Result = _postService.UpdatePost(postDTO);    
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
