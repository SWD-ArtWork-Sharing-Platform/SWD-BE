using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IPostService
    {
        Task<ResponseDTO> GettAllPost();
        Task<ResponseDTO> CreateNewPost(PostDTO postDTO);
        Task<ResponseDTO> GetPostByID(string id);
        Task<ResponseDTO> UpdatePostByID(PostDTO postDTO);  
        Task<ResponseDTO> DeletePostByID(string id);
        Task<ResponseDTO> GetPostByCondition(string title, string status, string artworkId);
    }
}
