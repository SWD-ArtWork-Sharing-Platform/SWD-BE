﻿using Auth.Data;
using Auth.Models.DTO;
using Auth.Models;
using Auth.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Auth.Helper;
using Microsoft.AspNetCore.Authorization;
using Auth.Util;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Auth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        protected ResponseDTO _response;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IAuthService authService,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            AppDbContext db,
            IJwtTokenGenerator jwtTokenGenerator,
            IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _authService = authService;
            _response = new();
            _configuration = configuration;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("register")]
        public async Task<ResponseDTO> Register([FromBody] RegisterationRequestDTO model)
        {
            //PhoneCheck

            string url = "https://phonevalidation.abstractapi.com/v1/?api_key=84e07dc23ee94971aefbc7bb087ab2f9&phone=" + model.PhoneNumber;
            var client = _httpClientFactory.CreateClient("PhoneCheck");
            var response = await client.GetAsync(url);
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<PhoneCheckDTO>(apiContent);
            if (!resp.valid)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Not valid phone number"
                };
            }

            string Eurl = "https://emailvalidation.abstractapi.com/v1/?api_key=9a8c7152718b4e6189e0a6e713fccb7d&email=" + model.Email;
            var Eclient = _httpClientFactory.CreateClient("MailCheck");
            var Eresponse = await Eclient.GetAsync(Eurl);
            var EapiContent = await Eresponse.Content.ReadAsStringAsync();
            var Eresp = JsonConvert.DeserializeObject<MailCheckDTO>(EapiContent);
            if (!(Eresp.is_smtp_valid.value ?? false))
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Not valid gmail!"
                };
            }


            ApplicationUser user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Status = SD.INACTIVE
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == model.Email);

                    UserDTO userDto = new()
                    {
                        Email = userToReturn.Email ?? "",
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber ?? "",

                    };
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = userDto.Email }, HttpContext.Request.Scheme);
                    string returnUrl = "http://localhost:3000/ConfirmEmail/" + userDto.Email + "/" + token;

                    string content = ContentMailUtil.GetContentRegisterAccount(returnUrl);
                    string sendMail = SendMail.SendEmail(user.Email, "Confirm your account", content, "");
                    if (sendMail != "")
                    {
                        _response.IsSuccess = false;
                        _response.Message = sendMail;
                    }
                }
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ResponseDTO> ConfirmEmail(string email, string token)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result = await _userManager.ConfirmEmailAsync(user, token);
                    user.Status = SD.ACTIVE;
                    _db.SaveChanges();
                    _response.Result = result;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
            return _response;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            else if (loginResponse.User.Status == SD.INACTIVE)
            {
                _response.IsSuccess = false;
                _response.Message = "Account is inactive!";
                return BadRequest(_response);
            }

            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("LoginGoogle")]
        public async Task<IActionResult> LoginGoogle([FromBody] string email)
        {
            var loginResponse = await _authService.LoginGoogle(email);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            else if (loginResponse.User.Status == SD.INACTIVE)
            {
                _response.IsSuccess = false;
                _response.Message = "Account is inactive!";
                return BadRequest(_response);
            }

            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo(string userID)
        {
            ApplicationUser assignRole = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userID);
            if (assignRole == null)
            {
                _response.IsSuccess = false;
                _response.Message = "User not found!";
                return BadRequest(_response);
            }
            else
            {
                _response.Result = assignRole;
            }
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterationRequestDTO model)
        {
            var assignRole = await _authService.AssignRole(model.Email, (SD.CheckRole(model.Role) ?? "").ToUpper());
            if (!assignRole)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("SendEmailForgotPassword")]
        public async Task<ResponseDTO> SendEmailForgotPassword(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null && user.Email != null)
                {

                    Random rand = new Random();
                    int randomNumber = rand.Next(10000, 99999);
                    string callbackurl = "http://localhost:3000/ResetPassword/" + email + "/" + randomNumber;

                    ID_F_ResetPass? reset = _db.ResetPasses.Find(email);
                    if (reset != null)
                    {
                        _db.ResetPasses.Remove(reset);
                        _db.SaveChanges();
                    }
                    ID_F_ResetPass newObj = new ID_F_ResetPass()
                    {
                        Gmail = email,
                        Code = randomNumber.ToString()
                    };
                    _db.ResetPasses.Add(newObj);
                    _db.SaveChanges();

                    string content = ContentMailUtil.GetContentForgetPassword(callbackurl);


                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    string sendMail = SendMail.SendEmail(user.Email, "Reset password", content, "");
                    if (sendMail != "")
                    {
                        _response.IsSuccess = false;
                        _response.Message = sendMail;
                    }
                    // _response.Result = Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "User not found!";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost("ChangePasswordWithOldPass")]
        public async Task<IActionResult> ChangePasswordWithOldPass(ChangePasswordDTO model)
        {
            var changePasswordRequest = await _authService.ChangePassword(model);
            if (changePasswordRequest == null)
            {
                _response.IsSuccess = false;
                _response.Message = "New password is incorrect";
            }
            _response.Result = changePasswordRequest;
            return Ok(_response);
        }
        [HttpPost("ChangePasswordWithoutOldPass")]
        public async Task<ResponseDTO> ChangePasswordWithoutOldPass(string email, string newpass, string code)
        {
            try
            {
                ID_F_ResetPass? confirm = _db.ResetPasses.FirstOrDefault(u => u.Gmail == email);
                if (confirm == null || confirm.exprired_time > DateTime.Now || confirm.Code != code)
                {
                    return new ResponseDTO()
                    {
                        IsSuccess = false,
                        Message = "Token is not valid! "
                    };
                }
                else
                {


                    _db.ResetPasses.Remove(confirm);
                    _db.SaveChanges();
                }

                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    // Trả về lỗi nếu không tìm thấy người dùng
                    return new ResponseDTO()
                    {
                        IsSuccess = false,
                        Message = "User not found!"
                    };
                }

                // Thực hiện thay đổi mật khẩu
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var changePasswordResult = await _userManager.ResetPasswordAsync(user, token, newpass);

                // Kiểm tra kết quả và trả về phản hồi phù hợp
                if (changePasswordResult.Succeeded)
                {
                    _response.Message = "Change successfully!";
                    return _response;
                }
                else
                {
                    return new ResponseDTO()
                    {
                        IsSuccess = false,
                        Message = "Change unsuccessfully!"
                    };
                }

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                return _response;
            }

        }

        [HttpPut("UpdateAccount")]
        public async Task<ResponseDTO> UpdateAccount(ApplicationUser applicationUser)
        {

            try
            {
                ApplicationUser? user = _db.ApplicationUsers.AsNoTracking().FirstOrDefault(
                    u => u.Id == applicationUser.Id);
                if (user == null)
                {
                    return new ResponseDTO()
                    {
                        IsSuccess = false,
                        Message = "User ID not found!"
                    };
                }
                user.Name = applicationUser.Name;
                user.Address = applicationUser.Address;
                user.Status = applicationUser.Status;
                _db.ApplicationUsers.Update(user);
                await _db.SaveChangesAsync();
                _response.Message = "Updated successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("ChangeAccountStatus")]
        public async Task<ResponseDTO> ChangeAccountStatus(string userID, string Status)
        {

            ApplicationUser? user = _db.ApplicationUsers.AsNoTracking().FirstOrDefault(
                    u => u.Id ==userID);
            if (user == null)
            {
                return new ResponseDTO()
                {
                    IsSuccess = false,
                    Message = "User ID not found!"
                };
            }
            user.Status = Status;
            _db.ApplicationUsers.Update(user);
            await _db.SaveChangesAsync();
            _response.Message = "Updated successfully";
            return _response;
        }
    }
}

