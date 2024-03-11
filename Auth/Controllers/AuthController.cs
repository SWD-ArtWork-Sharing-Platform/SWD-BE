using Auth.Data;
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
            
            string url = "https://phonevalidation.abstractapi.com/v1/?api_key=0b4a7b91b44e40648af2b59fb2e8190c&phone=" +model.PhoneNumber;
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

            string Eurl = "https://emailvalidation.abstractapi.com/v1/?api_key=6d23c9a3595f4aecbce5bffbeb1cd8e4&email=" + model.Email;
            var Eclient = _httpClientFactory.CreateClient("MailCheck");
            var Eresponse = await Eclient.GetAsync(Eurl);
            var EapiContent = await Eresponse.Content.ReadAsStringAsync();
            var Eresp = JsonConvert.DeserializeObject<MailCheckDTO>(EapiContent);
            if (!Eresp.is_smtp_valid.value)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Not valid phone number"
                };
            }


            ApplicationUser user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                Name = model.Name,
                PhoneNumber = model.PhoneNumber
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
                        PhoneNumber = userToReturn.PhoneNumber ?? ""
                    };
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(nameof(ConfirmEmail), "AuthAPI", new { token, email = userDto.Email }, HttpContext.Request.Scheme);
                    string sendMail = SendMail.SendEmail(user.Email, "Confirm your account",
                        "Please confirm your account by clicking <a href=\"" +
                        callbackUrl + "\">here</a>", "");
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
            _response.Result = loginResponse;
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

        [HttpPost("ForgotPassword")]
        public async Task<ResponseDTO> ForgotPassword(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null && user.Email != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var forgetLink = Url.Action(nameof(ForgotPassword), "AuthAPI", new { token, email = user.Email }, HttpContext.Request.Scheme);
                    string sendMail = SendMail.SendEmail(user.Email, "Reset password", "Please reset your password by clicking <a href=\"" + forgetLink + "\">here</a>", "");
                    if (sendMail != "")
                    {
                        _response.IsSuccess = false;
                        _response.Message = sendMail;
                    }
                    _response.Result = Ok(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("ResetPassword")]
        public async Task<ResponseDTO> ResetPassword(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null && user.Email != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetLink = Url.Action(nameof(ForgotPassword), "AuthAPI", new { token, email = user.Email }, HttpContext.Request.Scheme);
                    string sendMail = SendMail.SendEmail(user.Email, "Reset password", "Please reset your password by clicking <a href=\"" + resetLink + "\">here</a>", "");
                    if (sendMail != "")
                    {
                        _response.IsSuccess = false;
                        _response.Message = sendMail;
                    }
                    _response.Result = Ok(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
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

        [HttpPut("UpdateAccount")]
        [Authorize]
        public async Task<ResponseDTO> UpdateAccount(ApplicationUser applicationUser)
        {
            try
            {
                var updateRequest = await _authService.UpdateAccount(applicationUser);
                _response.Result = updateRequest;
                _response.Message = "Updated successfully";
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

