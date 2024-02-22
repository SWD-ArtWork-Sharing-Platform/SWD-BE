using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IPostService
    {
        ResponseDTO GetAllPost();
        ResponseDTO CreateNewPost(PostDTO postDTO);
        ResponseDTO GetPostByID(string id);
        ResponseDTO UpdatePost(PostDTO postDTO);  
        ResponseDTO DeletePostByID(string id);
        ResponseDTO GetPostByCondition(string title, string artworkId);
    }
}
