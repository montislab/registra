using Microsoft.AspNetCore.Mvc;
using RegistraWebApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistraWebApi.Services
{
    public interface IAdminService
    {
        public Task<List<UserWithRolesDto>> PrepareUsersWithRoles();
        public Task<IList<string>> EditRoles(RoleEditDto roleEditDto);
    }
}
