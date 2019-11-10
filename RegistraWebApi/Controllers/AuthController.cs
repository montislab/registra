using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistraWebApi.Dtos;
using RegistraWebApi.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using RegistraWebApi.Services;
using RegistraWebApi.Exceptions;
using System.Net;

namespace RegistraWebApi.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IAuthService authService;

        public AuthController(IConfiguration config, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IAuthService authService)
        {
            this.signInManager = signInManager;
            this.authService = authService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                User newUser = await authService.Register(mapper.Map<User>(userForRegisterDto), userForRegisterDto.Password);
                UserDto userToReturn = mapper.Map<UserDto>(newUser);
                return Ok(userToReturn);
            }
            catch (ErrorStatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode);
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            try
            {
                UserPublicDataWithJwtDto result = await authService.Login(userForLoginDto);
                return Ok(result);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
        }
    }
}