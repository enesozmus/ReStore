using AutoMapper;
using ReStore.Application.CQRS;
using ReStore.Domain.Entities;

namespace ReStore.Application.Mappings;

public class MappingProfile : Profile
{
        public MappingProfile()
        {
                #region Products

                CreateMap<Product, GetProductsQueryResponse>()
                                 .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.Name))
                                 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

                #endregion
        }
}

