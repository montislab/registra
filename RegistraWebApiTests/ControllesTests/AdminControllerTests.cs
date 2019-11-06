using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using RegistraWebApi.Controllers;
using RegistraWebApi.Models;
using RegistraWebApi.Services;
using Xunit;
using Moq;
using RegistraWebApi.Dtos;
using RegistraWebApi.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RegistraWebApiTests.ControllesTests
{
    public class AdminControllerTests
    {
        [Fact]
        public void AdminControled_IsJustCreated()
        {
            Mock<UserManager<User>> userManagerMock = PrepareUserManagerMock();
            Mock<RoleManager<Role>> roleManagerMock = PrepareRoleManagerMock();

            Mock<AdminService> adminService = new Mock<AdminService>(userManagerMock.Object, roleManagerMock.Object);
            AdminController adminController = new AdminController(adminService.Object);

            adminController
                .Should()
                .NotBeNull();
        }

        private Mock<UserManager<User>> PrepareUserManagerMock()
        {
            Mock<IUserStore<User>> userStoreMock = new Mock<IUserStore<User>>();
            Mock<UserManager<User>> userManagerMock = new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Object.UserValidators.Add(new UserValidator<User>());
            userManagerMock.Object.PasswordValidators.Add(new PasswordValidator<User>());

            return userManagerMock;
        }

        private static Mock<RoleManager<Role>> PrepareRoleManagerMock()
        {
            var roleStoreMock = new Mock<IRoleStore<Role>>();
            var roleManagerMock = new Mock<RoleManager<Role>>(roleStoreMock.Object, null, null, null, null);

            return roleManagerMock;
        }

        [Fact]
        public void GetUserWithRoles_ReturnsEmptyCollection()
        {
            Mock<UserManager<User>> userManagerMock = PrepareUserManagerMock();
            Mock<RoleManager<Role>> roleManagerMock = PrepareRoleManagerMock();

            Mock<IAdminService> adminService = new Mock<AdminService>(userManagerMock.Object, roleManagerMock.Object)
                .As<IAdminService>();
            adminService.Setup(mas => mas.PrepareUsersWithRoles()).Returns(Task.FromResult(new List<UserWithRolesDto>()));

            AdminController adminController = new AdminController(adminService.Object);

            IActionResult result = adminController.GetUsersWithRoles().Result;

            result
                .Should()
                .BeOfType<OkObjectResult>();

            OkObjectResult okObjectResult = result as OkObjectResult;
            okObjectResult.Value
                .Should()
                .BeEquivalentTo(new List<UserWithRolesDto>());
        }

        [Fact]
        public void GetUserWithRoles_ReturnsListOdUsersWithRoles()
        {
            Mock<UserManager<User>> userManagerMock = PrepareUserManagerMock();
            Mock<RoleManager<Role>> roleManagerMock = PrepareRoleManagerMock();

            Mock<IAdminService> adminService = new Mock<AdminService>(userManagerMock.Object, roleManagerMock.Object)
                .As<IAdminService>();
            adminService.Setup(mas => mas.PrepareUsersWithRoles()).Returns(Task.FromResult(PrepareDummyUsersWithRoles()));

            AdminController adminController = new AdminController(adminService.Object);

            IActionResult result = adminController.GetUsersWithRoles().Result;

            result
                .Should()
                .BeOfType<OkObjectResult>();

            OkObjectResult okObjectResult = result as OkObjectResult;
            okObjectResult.Value
                .Should()
                .BeEquivalentTo(PrepareDummyUsersWithRoles());
        }

        private List<UserWithRolesDto> PrepareDummyUsersWithRoles()
        {
            return new List<UserWithRolesDto>()
            {
                new UserWithRolesDto()
                {
                    Id = 1,
                    UserName = "admin@admin.com",
                    Roles = new string[]
                    {
                        RoleNames.Client,
                        RoleNames.Admin
                    }
                },
                new UserWithRolesDto()
                {
                    Id = 2,
                    UserName = "asd@asd.com",
                    Roles = new string[]
                    {
                        RoleNames.Client
                    }
                }
            };
        }
    }
}
