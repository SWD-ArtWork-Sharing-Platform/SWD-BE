using AutoMapper;
using Management.Data;
using Management.Models;
using Management.Models.DTO;
using Management.Repository.IRepository;
using Management.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace Management.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationRepository _configurationRepository;
        public ConfigurationService(ArtworkSharingPlatformContext db, IMapper mapper, IConfiguration configuration, IConfigurationRepository configurationRepository)
        {
            this._response = new ResponseDTO();
            _mapper = mapper;
            _db = db;
            _configuration = configuration;
            _configurationRepository = configurationRepository;
        }
        public ResponseDTO CreateNewConfiguration(ConfigurationDTO configurationDTO)
        {
            try
            {
                FConfiguration configuration = _mapper.Map<FConfiguration>(configurationDTO);
                _configurationRepository.Add(configuration);
                _configurationRepository.Save();
                _response.Result = _mapper.Map<ConfigurationDTO>(configuration);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO DeleteConfigurationByID(string id, bool confirm)
        {
            try
            {
                if (confirm)
                {
                    FConfiguration configuration = _db.FConfigurations.First(u => u.ConfigurationId == id);
                    _configurationRepository.Remove(configuration);
                    _configurationRepository.Save();
                    _response.Result = _mapper.Map<ConfigurationDTO>(configuration);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;

        }

        public ResponseDTO GetAllConfiguration()
        {
            try
            {
                IEnumerable<FConfiguration> configurationList = _configurationRepository.GetAll();
                _response.Result = _mapper.Map<IEnumerable<ConfigurationDTO>>(configurationList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetConfigurationByID(string id)
        {
            try
            {
                FConfiguration configuration = _configurationRepository.Get(u => u.ConfigurationId == id);
                _response.Result = _mapper.Map<ConfigurationDTO>(configuration);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO UpdateConfiguration(ConfigurationDTO configurationDTO)
        {
            try
            {
                FConfiguration configuration = _mapper.Map<FConfiguration>(configurationDTO);
                FConfiguration oldConfiguration = _db.FConfigurations.First(u => u.ConfigurationId == configurationDTO.ConfigurationId);
                if (oldConfiguration == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Configuration not found!";
                }
                else
                {
                    _configurationRepository.Update(oldConfiguration);
                    _configurationRepository.Save();
                    _response.Message = "Update successfully!";
                }
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
