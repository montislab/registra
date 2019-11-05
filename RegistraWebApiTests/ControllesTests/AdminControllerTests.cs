using Microsoft.AspNetCore.Identity;
using RegistraWebApi.Controllers;
using RegistraWebApi.Models;
using RegistraWebApi.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RegistraWebApiTests.ControllesTests
{
    public class AdminControllerTests
    {
        [Fact]
        public void AdminControled_IsJustCreated()
        {
            IAdminService adminService = null;  //TODO: create real service
            AdminController adminController = new AdminController(adminService);

            Assert.NotNull(adminController);
        }
    }
}
