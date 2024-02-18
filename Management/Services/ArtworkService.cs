using Management.Data;
using Management.Models;
using Management.Models.DTO;
using Management.Services.IService;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Management.Services
{
    public class ArtworkService : IArtworkService
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public ArtworkService(ArtworkSharingPlatformContext db, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            this._response = new ResponseDTO();
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<ResponseDTO> CreateNewArtwork(ArtworkDTO artworkDTO)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public Task<ResponseDTO> DeleteArtWorkByID(string id, bool confirm)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> GetAllCategory()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> GetArtWorkByCondition(string? name, string id, string status, string discount)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> GetArtworkById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> ReportArtworkByID(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> UpdateArtwork(ArtworkDTO artworkDTO)
        {
            throw new NotImplementedException();
        }
    }
}
