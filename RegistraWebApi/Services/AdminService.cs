using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistraWebApi.Dtos;
using RegistraWebApi.Exceptions;
using RegistraWebApi.Models;

namespace RegistraWebApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public AdminService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<List<UserWithRolesDto>> PrepareUsersWithRoles()
        {
            return await userManager.Users
                .OrderBy(u => u.Id)
                .Select(user => new UserWithRolesDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = userManager.GetRolesAsync(user).Result
                }).ToListAsync();
        }

        public async Task<IList<string>> EditRoles(RoleEditDto roleEditDto)
        {
            User user = await userManager.FindByNameAsync(roleEditDto.UserName);

            if (user == null)
                throw new BadRequestException("User not exist");

            IList<string> userRoles = await userManager.GetRolesAsync(user);
            string[] selectedRoles = roleEditDto.RoleNames ?? new string[] { };
            List<string> availableRoles = roleManager.Roles.Select(r => r.Name).ToList();

            if (selectedRoles.Any(r => !availableRoles.Contains(r)))
                throw new BadRequestException("Invalid roles");

            IdentityResult result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                throw new BadRequestException("Fail to add to roles");

            result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                throw new BadRequestException("Fail to remove from roles");

            return await userManager.GetRolesAsync(user);
        }
    }
}
