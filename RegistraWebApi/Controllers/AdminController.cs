using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AdminController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersWithRoles")]
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
    }
}
