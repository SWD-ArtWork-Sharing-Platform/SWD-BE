using Auth.Models;
using Auth.Models.DTO;

namespace Auth.Services.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LoginResponseDTO> LoginGoogle(string email);
        Task<bool> AssignRole(string email, string roleName);
        Task<ChangePasswordDTO> ChangePassword(ChangePasswordDTO changePasswordDTO);
        Task<ApplicationUser> UpdateAccount(ApplicationUser updateUser);
        Task<ResponseDTO> ChangePass(string email, string currentPassword, string newPassword);


    }
}
