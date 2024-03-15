using AutoMapper;
using Management.Data;
using Management.Models;
using Management.Models.DTO;
using Management.Repository.IRepository;
using Management.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace Management.Services
{
    public class UserServices : IUserServices
    {

        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserServices(ArtworkSharingPlatformContext db, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._response = new ResponseDTO();
            _mapper = mapper;
            _db = db;
            _userManager = userManager;
        }
        public async Task<ResponseDTO> UserReportManagement(string UserID)
        {
            IEnumerable<ApplicationUser> User = _userManager.Users.ToList();

            UserReportDTO returnObj = new UserReportDTO()
            {
                ExportBy = UserID,
                ExportDate = DateTime.Now,
                TotalUser  =User.Count(),
                userActive = User.Count(u=>u.Status =="Active"),
                userInactive = User.Count(u=>u.Status =="Inactive"),

                ListUser = User.ToList()
            };

            _response.Result = returnObj;
            return _response;
        }
    }
}
