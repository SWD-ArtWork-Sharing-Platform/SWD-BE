using Management.Models;
using Management.Models.DTO;
using AutoMapper;

namespace Management
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<FArtwork, ArtworkDTO>().ReverseMap();
                config.CreateMap<FConfiguration, ConfigurationDTO>().ReverseMap();
                config.CreateMap<DInteraction, InteractionDTO>().ReverseMap();
                config.CreateMap<DPackageOfCreator, PackageOfCreatorDTO>().ReverseMap();
                config.CreateMap<FPackage, PackageDTO>().ReverseMap();
                config.CreateMap<FPost, PostDTO>().ReverseMap();
                config.CreateMap<FReport, ReportDTO>().ReverseMap();
                config.CreateMap<DCategory, CategoryDTO>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
