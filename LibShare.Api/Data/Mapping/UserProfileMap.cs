using AutoMapper;
using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.DTO;
using LibShare.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Mapping
{
    public class UserProfileMap : Profile
    {
        public UserProfileMap()
        {
            AllowNullCollections = true;
            CreateMap<DbUser, UserDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserProfile == null ? "unknown" : src.UserProfile.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.UserProfile == null ? "unknown" : src.UserProfile.Surname))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.UserProfile.Photo ?? null))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.UserProfile.Phone ?? null))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.UserProfile.DateOfBirth ?? null));

            CreateMap<UserApiModel, UserDTO>().ReverseMap();

            CreateMap<DbUser, UserApiModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserProfile == null ? "unknown" : src.UserProfile.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.UserProfile == null ? "unknown" : src.UserProfile.Surname))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.UserProfile.Photo ?? null))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.UserProfile.Phone ?? null))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.UserProfile.DateOfBirth ?? null));
        }
    }
}
