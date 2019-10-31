using AutoMapper;
using RegistraWebApi.Dtos;
using RegistraWebApi.Models;

namespace RegistraWebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForLoginDto, User>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.LoginEmail));
            CreateMap<User, UserForLoginDto>().ForMember(dest => dest.LoginEmail, opt => opt.MapFrom(src => src.UserName));

            CreateMap<UserForRegisterDto, User>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.LoginEmail));
            CreateMap<User, UserForRegisterDto>().ForMember(dest => dest.LoginEmail, opt => opt.MapFrom(src => src.UserName));

            CreateMap<UserDto, User>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.LoginEmail));
            CreateMap<User, UserDto>().ForMember(dest => dest.LoginEmail, opt => opt.MapFrom(src => src.UserName));
        }
    }
}