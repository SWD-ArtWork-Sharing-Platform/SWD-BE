
using AutoMapper;
using Market.Models;
using Market.Models.DTO;
using System.ComponentModel;

namespace Market
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {

                // DCategory
                config.CreateMap<DCategory, CategoryDTO>()
                    .ForPath(dest => dest.UpdatedBy.Id, opt => opt.MapFrom(src => src.UpdatedBy));

                config.CreateMap<CategoryDTO, DCategory>()
                   .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy.Id));

                // DOrderDetail
                config.CreateMap<DOrderDetail, OrderDetailDTO>()
                    .ForPath(dest => dest.ArtworkId.ArtworkId, opt => opt.MapFrom(src => src.ArtworkId));

                config.CreateMap<OrderDetailDTO, DOrderDetail>()
                   .ForMember(dest => dest.ArtworkId, opt => opt.MapFrom(src => src.ArtworkId.ArtworkId));

                //DPackageOfCreator
                config.CreateMap<DPackageOfCreator, PackageOFCreatorDTO>().ReverseMap();

                // DOrderDetail
                config.CreateMap<FArtwork, ArtWorkDTO>()
                    .ForPath(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryId))
                    .ForPath(dest => dest.Creator.Id, opt => opt.MapFrom(src => src.Id));

                config.CreateMap<ArtWorkDTO, FArtwork>()
                   .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryID))
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Creator.Id));

                config.CreateMap<FOrder, OrderDTO>().ReverseMap();
                config.CreateMap<FPackage, PackageDTO>().ReverseMap();

                // DOrderDetail
          

                config.CreateMap<FWishlist, WishListDTO>().ReverseMap();
                config.CreateMap<DWishlistDetail, WishListDetailDTO>().ReverseMap();
                config.CreateMap<DBankAccount, BankAccountDTO>().ReverseMap();


            });
            return mappingConfig;
        }
    }
}
