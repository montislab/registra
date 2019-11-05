using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistraWebApi.Constants;
using RegistraWebApi.Dtos;
using RegistraWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistraWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public AdminController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [Authorize(Policy = PolicyNames.RequireAdminRole)]
        [HttpGet("getUsersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles() => Ok(await PrepareUsersWithRoles());

        private async Task<List<UserWithRolesDto>> PrepareUsersWithRoles()
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

        [Authorize(Policy = PolicyNames.RequireAdminRole)]
        [HttpPost("editRoles")]
        public async Task<IActionResult> EditRoles(RoleEditDto roleEditDto)
        {
            User user = await userManager.FindByNameAsync(roleEditDto.UserName);

            if (user != null)
            {
                IList<string> userRoles = await userManager.GetRolesAsync(user);
                string[] selectedRoles = roleEditDto.RoleNames ?? new string[] { };
                List<string> availableRoles = roleManager.Roles.Select(r => r.Name).ToList();

                if (selectedRoles.Any(r => !availableRoles.Contains(r)))
                    return BadRequest("Invalid roles");

                IdentityResult result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

                if (!result.Succeeded)
                    return BadRequest("Fail to add to roles");

                result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

                if (!result.Succeeded)
                    return BadRequest("Fail to remove from roles");

                return Ok(await userManager.GetRolesAsync(user));
            }

            return BadRequest("User not exist");
        }
    }
}
