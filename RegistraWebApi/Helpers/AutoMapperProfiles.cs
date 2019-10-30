using AutoMapper;
using RegistraWebApi.Dtos;
using RegistraWebApi.Models;

namespace RegistraWebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForRegisterDto>();
        }
    }
}