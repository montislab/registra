﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistraWebApi.Constants;
using RegistraWebApi.Dtos;
using RegistraWebApi.Exceptions;
using RegistraWebApi.Services;
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
        public async Task<IActionResult> GetUsersWithRoles() => Ok(await adminService.PrepareUsersWithRoles());

        [Authorize(Policy = PolicyNames.RequireAdminRole)]
        [HttpPost("editRoles")]
        public async Task<IActionResult> EditRoles(RoleEditDto roleEditDto)
        {
            try
            {
                return Ok(await adminService.EditRoles(roleEditDto));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
