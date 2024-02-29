using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface ICategoryService 
    {
        ResponseDTO GetAllCategory();
        ResponseDTO CreateCategory(CategoryDTO categoryDTO);
        ResponseDTO UpdateCategory(CategoryDTO categoryDTO);    
        ResponseDTO DeleteCategory(string categoryId);
        ResponseDTO GetCategoryByCondition(string name, string type);
    }
}
