using Management.Models;
using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IUserServices
    {
        Task<ResponseDTO> UserReportManagement(string userID);
        ResponseDTO UpdateAccount(ApplicationUser user);
    }
}
