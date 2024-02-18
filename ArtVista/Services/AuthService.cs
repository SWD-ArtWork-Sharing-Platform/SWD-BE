using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ArtVistaAuthAPI.Models.DTO;
using ArtVistaAuthAPI.Data;
using ArtVistaAuthAPI.Models;
using ArtVistaAuthAPI.Services.IService;
using ArtVistaAuthAPI.Repository.IRepository;

namespace ArtVistaAuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IAccountRepository _accountRepository;
        public AuthService(AppDbContext db,
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAccountRepository accountRepository)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _accountRepository = accountRepository;
        }
        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => (u.Email ?? "").ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<ChangePasswordDTO> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => (u.Email ?? "").ToLower() == changePasswordDTO.Email.ToLower());
            if (user != null && changePasswordDTO.OldPassword != null && changePasswordDTO.NewPassword != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);
            }

            return changePasswordDTO;
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => (u.UserName ?? "").ToLower() == loginRequestDTO.Username.ToLower());
            bool isValid = false;
            if (user != null)
            {
                isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            };

            if (user == null || isValid == false)
            {
                return new LoginResponseDTO()
                {
                    User = null,
                    Token = ""
                };
            }
            else
            {
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles);

                UserDTO userDTO = new()
                {
                    Email = user.Email ?? "",
                    Id = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber ?? ""
                };
                LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
                {
                    User = userDTO,
                    Token = token
                };

                return loginResponseDTO;
            }
        }

        public async Task<ApplicationUser> UpdateAccount(ApplicationUser updateUser)
        {
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == updateUser.Email);
            if (user != null)
            {
                _accountRepository.Update(updateUser);
                _accountRepository.Save();
            }
            return updateUser;
        }



    }
}
