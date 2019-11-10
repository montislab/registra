using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RegistraWebApi.Constants;
using RegistraWebApi.Dtos;
using RegistraWebApi.Exceptions;
using RegistraWebApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using System.Net;

namespace RegistraWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AuthService(IConfiguration config, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.config = config;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<User> Register(User userForRegister, string password)
        {
            if (userForRegister is null || string.IsNullOrEmpty(userForRegister.UserName))
                throw new BadRequestException("User identifyer is necessary");

            IdentityResult result = await userManager.CreateAsync(userForRegister, password);
            if (result.Succeeded)
            {
                if (await AddNewUserToRole(userForRegister))
                    return await userManager.FindByNameAsync(userForRegister.UserName);
                else
                    throw new ErrorStatusCodeException(HttpStatusCode.InternalServerError);
            }
            else
                throw new BadRequestException();
        }

        private async Task<bool> AddNewUserToRole(User userToCreate)
        {
            if (userToCreate is null)
                throw new BadRequestException("User not exist");

            User newUser = await userManager.FindByNameAsync(userToCreate.UserName);

            if (newUser != null &&
                (await userManager.AddToRoleAsync(newUser, RoleNames.Client)).Succeeded)
                return true;

            return false;
        }

        public async Task<UserPublicDataWithJwtDto> Login(UserForLoginDto userForLoginDto)
        {
            User user = await userManager.FindByNameAsync(userForLoginDto.LoginEmail);

            if (user is null)
                throw new BadRequestException();

            SignInResult result = await signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                UserDto appUser = mapper.Map<UserDto>(user);

                return new UserPublicDataWithJwtDto()
                {
                    token = GenerateJwtToken(user).Result,
                    user = appUser
                };
            }
            else
                throw new UnauthorizedException();
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            List<Claim> claims = PrepareBasicInfoClaims(user);
            await AddUserRolesToClaims(user, claims);
            SecurityTokenDescriptor tokenDescriptor = PrepareTokenDescriptor(claims);
            string preparedToken = PrepareTokenAsString(tokenDescriptor);

            return preparedToken;
        }

        private static List<Claim> PrepareBasicInfoClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
        }

        private async Task AddUserRolesToClaims(User user, List<Claim> claims)
        {
            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        private SecurityTokenDescriptor PrepareTokenDescriptor(List<Claim> claims)
        {
            //TODO this token from appsettings.json file has to be changed before app publishing because of security reasons
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection(AppSettingsNodes.JwtNode).Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            return tokenDescriptor;
        }

        private static string PrepareTokenAsString(SecurityTokenDescriptor tokenDescriptor)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
