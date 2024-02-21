using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IPostService
    {
        ResponseDTO GettAllPost();
        ResponseDTO CreateNewPost(PostDTO postDTO);
        ResponseDTO GetPostByID(string id);
        ResponseDTO UpdatePostByID(PostDTO postDTO);  
        ResponseDTO DeletePostByID(string id);
        ResponseDTO GetPostByCondition(string title, string artworkId);
    }
}
