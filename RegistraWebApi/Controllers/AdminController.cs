using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistraWebApi.Constants;
using RegistraWebApi.Dtos;
using RegistraWebApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegistraWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService) => this.adminService = adminService;

        [Authorize(Policy = PolicyNames.RequireAdminRole)]
        [HttpGet("getUsersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles() => Ok(await PrepareUsersWithRoles());

        private async Task<List<UserWithRolesDto>> PrepareUsersWithRoles() => await adminService.PrepareUsersWithRoles();

        [Authorize(Policy = PolicyNames.RequireAdminRole)]
        [HttpPost("editRoles")]
        public async Task<IActionResult> EditRoles(RoleEditDto roleEditDto)
        {
            try
            {
                return Ok(await adminService.EditRoles(roleEditDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
