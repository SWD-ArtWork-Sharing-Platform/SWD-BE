
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
                    .ForPath(dest => dest.Category.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

                config.CreateMap<ArtWorkDTO, FArtwork>()
                   .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.CategoryId));

                config.CreateMap<FOrder, OrderDTO>().ReverseMap();
                config.CreateMap<FPackage, PackageDTO>().ReverseMap();

                // DOrderDetail
                config.CreateMap<FPayment, PaymentDTO>()
                    .ForPath(dest => dest.Order.OrderId, opt => opt.MapFrom(src => src.OrderId));

                config.CreateMap<PaymentDTO, FPayment>()
                   .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.OrderId));

                config.CreateMap<FWishlist, WishListDTO>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
