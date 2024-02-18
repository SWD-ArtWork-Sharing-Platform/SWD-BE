using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IConfigurationService
    {
        Task<ResponseDTO> GetAllConfiguration();
        Task<ResponseDTO> GetConfigurationByID(string id); 
        Task<ResponseDTO> DeleteConfigurationByID(string id, bool confirm);
        Task<ResponseDTO> CreateNewConfiguration(ConfigurationDTO configurationDTO);
        Task<ResponseDTO> UpdateConfiguration(ConfigurationDTO configurationDTO);   
    }
}
