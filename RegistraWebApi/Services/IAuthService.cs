using RegistraWebApi.Dtos;
using RegistraWebApi.Models;
using System.Threading.Tasks;

namespace RegistraWebApi.Services
{
    public interface IAuthService
    {
        public Task<User> Register(User userForRegister, string password);
        public Task<UserPublicDataWithJwtDto> Login(UserForLoginDto userForLoginDto);
    }
}
