using ArtVistaAuthAPI.Models;
using ArtVistaAuthAPI.Models.DTO;

namespace ArtVistaAuthAPI.Services.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<bool> AssignRole(string email, string roleName);
        Task<ChangePasswordDTO> ChangePassword(ChangePasswordDTO changePasswordDTO);
        Task<ApplicationUser> UpdateAccount(ApplicationUser updateUser);
    }
}
