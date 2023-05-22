using AutoMapper;
using Talabat.Apis.DTOS;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;

namespace Talabat.Apis.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d=>d.PictureUrl, o=>o.MapFrom<ProductPictureURLResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
