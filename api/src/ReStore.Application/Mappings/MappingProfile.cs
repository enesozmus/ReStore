using AutoMapper;
using ReStore.Application.DTOs;
using ReStore.Domain.Entities;

namespace ReStore.Application.Mappings;

public class MappingProfile : Profile
{
      public MappingProfile()
      {
            #region Users

            CreateMap<AppUser, LoginCommandResponse>().ReverseMap();
            CreateMap<AppUser, CurrentUserQueryResponse>().ReverseMap();

            #endregion
      }
}

