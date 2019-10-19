using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistraWebApi.Dtos;
using RegistraWebApi.Models;
using RegistraWebApi.Persistance;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace RegistraWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration config;
        public AuthController(IUnitOfWork unitOfWork, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await unitOfWork.AuthRepository.Login(userForLoginDto.LoginEmail.ToLower(), userForLoginDto.Password);

            if(userFromRepo == null)
                return Unauthorized();
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.LoginEmail)
            };

            //TODO this token from appsettings.json file has to be changed before app publishing because of security reasons
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
                });
        }

    }
}