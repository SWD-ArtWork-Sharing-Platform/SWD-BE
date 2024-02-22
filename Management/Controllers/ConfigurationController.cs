using Management.Models.DTO;
using Management.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Route("api/configuration")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        protected ResponseDTO _response;
        private  IConfigurationService _configurationService;   
        public ConfigurationController(IConfigurationService configurationService)
        {
            this._response = new ResponseDTO();
            _configurationService = configurationService;   
        }

        [HttpGet("GetAllConfiguration")]
        public ResponseDTO GetAllConfiguration()
        {
            try
            {
                _response.Result =  _configurationService.GetAllConfiguration(); 
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;   
        }

        [HttpGet("GetConfigurationByID")]
        public ResponseDTO GetConfigurationByID(string id)
        {
            try
            {
                _response.Result =  _configurationService.GetConfigurationByID(id);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("CreateNewConfiguration")]
        public ResponseDTO CreateNewConfiguration(ConfigurationDTO configurationDTO)
        {
            try
            {
                _response.Result =  _configurationService.CreateNewConfiguration(configurationDTO);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("UpdateConfiguration")]
        public ResponseDTO UpdateConfiguration(ConfigurationDTO configurationDTO)
        {
            try
            {
                _response.Result =  _configurationService.UpdateConfiguration(configurationDTO);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeleteConfigurationByID")]
        public ResponseDTO DeleteConfigurationByID(string id, bool confirm)
        {
            try
            {
                _response.Result =  _configurationService.DeleteConfigurationByID(id,confirm);
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
