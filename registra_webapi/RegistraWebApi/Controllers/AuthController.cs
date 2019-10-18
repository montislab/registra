using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistraWebApi.Dtos;
using RegistraWebApi.Models;
using RegistraWebApi.Persistance;

namespace RegistraWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public AuthController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.LoginEmail = userForRegisterDto.LoginEmail.ToLower();

            if (await unitOfWork.AuthRepository.UserExists(userForRegisterDto.LoginEmail))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                LoginEmail = userForRegisterDto.LoginEmail
            };

            var createdUser = await unitOfWork.AuthRepository.Register(userToCreate, userForRegisterDto.Password);
            await unitOfWork.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return  StatusCode(200);
        }
    }
}