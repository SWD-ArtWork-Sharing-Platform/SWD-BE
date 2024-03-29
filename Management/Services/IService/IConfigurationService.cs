﻿using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IConfigurationService
    {
        ResponseDTO GetAllConfiguration();
        ResponseDTO GetConfigurationByID(string id); 
        ResponseDTO DeleteConfigurationByID(string id);
        ResponseDTO CreateNewConfiguration(ConfigurationDTO configurationDTO);
        ResponseDTO UpdateConfiguration(ConfigurationDTO configurationDTO);   
    }
}
