using AutoMapper;
using Management.Data;
using Management.Models;
using Management.Models.DTO;
using Management.Repository.IRepository;
using Management.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace Management.Services
{
    public class CategoryService : ICategoryService
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private  ICategoryRepository _categoryRepository;   
        public CategoryService(ArtworkSharingPlatformContext db, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._response = new ResponseDTO(); 
            _db = db;   
            _categoryRepository = categoryRepository;   
            _mapper = mapper;   
        }
        public ResponseDTO CreateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                DCategory category = _mapper.Map<DCategory>(categoryDTO);
                _categoryRepository.Add(category);
                _response.Result = _mapper.Map<CategoryDTO>(category);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO DeleteCategory(string categoryId)
        {
            try
            {
               
                DCategory? category = _db.DCategory.AsNoTracking().FirstOrDefault(u => u.CategoryId == categoryId);

                if (category == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No category found!";
                }
                else
                {
                    _db.DCategory.Remove(category);
                    _db.SaveChanges();
                    _response.Message = "Delete successfully!";
                }                
            }
            catch (Exception ex)
            {
                _response.Result = _db.FArtworks.Where( u => u.CategoryId == categoryId);
                _response.IsSuccess = false;
                _response.Message = "Another artwork belong to this category!";
            }
            return _response;
        }

        public ResponseDTO GetAllCategory()
        {
            try
            {
              IEnumerable<DCategory> categories = _categoryRepository.GetAll();
                _response.Result = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetCategoryByCondition(string name, string type)
        {
            try
            {
                IEnumerable<DCategory> categories = _categoryRepository.GetAll();
                IEnumerable<CategoryDTO> categoryDTOList = new List<CategoryDTO>();

                if (!string.IsNullOrEmpty(name))
                {
                    categories = categories.Where(u => u.CategoryName == name);
                }
                if (!string.IsNullOrEmpty(type))
                {
                    categories = categories.Where(u => u.Type == type);
                }
                if (categories != null)
                {
                    categoryDTOList = _mapper.Map<IEnumerable<CategoryDTO>>(categories); 
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;   
        }


        public ResponseDTO UpdateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                DCategory category = _mapper.Map<DCategory>(categoryDTO);
                DCategory? oldCategory = _db.DCategory.AsNoTracking().FirstOrDefault(u => u.CategoryId == categoryDTO.CategoryId);

                if (oldCategory == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No category found!";
                }
                else
                {
                    oldCategory = category;
                    _categoryRepository.Update(oldCategory);
                    _categoryRepository.Save();
                    _response.Message = "Update successfully!";
                }
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
