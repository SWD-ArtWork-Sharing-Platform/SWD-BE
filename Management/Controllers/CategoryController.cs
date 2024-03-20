using Management.Models.DTO;
using Management.Repository.IRepository;
using Management.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        protected ResponseDTO _response;
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this._response = new ResponseDTO();
            _categoryService = categoryService;
        }

        [HttpGet("GetAllCategory")]
        public ResponseDTO GetAllCategory()
        {
            try
            {
                _response.Result = _categoryService.GetAllCategory();   
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize(Policy = "ARTWORKMANAGEMENT")]
        [HttpDelete("DeleteCategory")]
        public ResponseDTO DeleteCategory(string categoryId)
        {
            try
            {
                _response = _categoryService.DeleteCategory(categoryId);     
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

       [HttpPost("CreateCategory")]
        //[Authorize(Policy = "ARTWORKMANAGEMENT")]
        public ResponseDTO CreateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                _response.Result = _categoryService.CreateCategory(categoryDTO);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetCategoryByCondition")]
        public ResponseDTO GetCategoryByCondition(string name, string type)
        {
            try
            {
                _response = _categoryService.GetCategoryByCondition(name, type);     
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("UpdateCategory")]
        //[Authorize(Policy = "ARTWORKMANAGEMENT")]
        public ResponseDTO UpdateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                _response.Result = _categoryService.UpdateCategory(categoryDTO);
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
