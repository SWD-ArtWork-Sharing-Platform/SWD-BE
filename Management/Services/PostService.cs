using AutoMapper;
using Azure;
using Management.Data;
using Management.Models;
using Management.Models.DTO;
using Management.Repository.IRepository;
using Management.Services.IService;

namespace Management.Services
{
    public class PostService : IPostService
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private IPostRepository _postRepository;
        public PostService(ArtworkSharingPlatformContext db, IMapper mapper, IConfiguration configuration, IPostRepository postRepository)
        {
            this._response = new ResponseDTO();
            _mapper = mapper;
            _db = db;
            _postRepository = postRepository;
        }
        public ResponseDTO CreateNewPost(PostDTO postDTO)
        {
            try
            {
                FPost post = _mapper.Map<FPost>(postDTO);
                post.PostId = DateTime.Now.ToString();
                _postRepository.Add(post);
                _postRepository.Save();
                _response.Result = _mapper.Map<PostDTO>(post);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO DeletePostByID(string id)
        {
            try
            {
                FPost post = _postRepository.Get(u => u.PostId == id);
                if (post == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No post found";
                }
                else
                {
                    _postRepository.Remove(post);
                    _postRepository.Save();
                    _response.Result = _mapper.Map<PostDTO>(post);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetPostByCondition(string title, string artworkId)
        {
            try
            {
                IEnumerable<FPost> postList = _postRepository.GetAll();
                IEnumerable<PostDTO> postListDTO = new List<PostDTO>();
                if (!string.IsNullOrEmpty(title))
                {
                    postList = postList.Where(u => u.Tittle == title);
                }
                if (!string.IsNullOrEmpty(artworkId))
                {
                    postList = postList.Where(u => u.ArtworkId == artworkId);
                }
                if (postList != null)
                {
                    postListDTO = _mapper.Map<IEnumerable<PostDTO>>(postList);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetPostByID(string id)
        {
            try
            {
                FPost post = _postRepository.Get(u => u.PostId == id);
                _response.Result = _mapper.Map<PostDTO>(post);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetAllPost()
        {
            try
            {
                IEnumerable<FPost> postlist = _postRepository.GetAll();
                _response.Result = _mapper.Map<IEnumerable<PostDTO>>(postlist);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO UpdatePost(PostDTO postDTO)
        {
            try
            {
                FPost post = _mapper.Map<FPost>(postDTO);
                FPost oldPost = _postRepository.Get(u => u.PostId == postDTO.PostId);
                if (post == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No post found";
                }
                else
                {
                    oldPost = post;
                    oldPost.Artwork = null;
                    _postRepository.Update(post);
                    _postRepository.Save();
                    _response.Result = _mapper.Map<PostDTO>(post);
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
