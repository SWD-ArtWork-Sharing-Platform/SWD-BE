using Management.Models;
using Management.Models.DTO;
using Management.Services;
using Management.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsermanagementController : ControllerBase
    {
        protected ResponseDTO _response;
        private IUserServices _userServices;

        public UsermanagementController(IUserServices artworkService)
        {
            this._response = new ResponseDTO();
            _userServices = artworkService;
        }

        [Authorize(Policy = "ARTWORKMANAGEMENT")]
        [HttpPost("UserReportManagement")]
        public ResponseDTO UserReportManagement(string UserID)
        {
            try
            {
                _response.Result = _userServices.UserReportManagement(UserID);
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
