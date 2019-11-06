// using FluentAssertions;
// using Microsoft.AspNetCore.Identity;
// using RegistraWebApi.Controllers;
// using RegistraWebApi.Models;
// using RegistraWebApi.Services;
// using Xunit;
// using Moq;

// namespace RegistraWebApiTests.ControllesTests
// {
//     public class AdminControllerTests
//     {
//         [Fact]
//         public void AdminControled_IsJustCreated()
//         {
//             Mock<UserManager<User>> userManagerMock = PrepareUserManagerMock();
//             Mock<RoleManager<Role>> roleManagerMock = PrepareRoleManagerMock();

//             Mock<AdminService> adminService = new Mock<AdminService>(userManagerMock.Object, roleManagerMock.Object);
//             AdminController adminController = new AdminController(adminService.Object);

//             adminController
//                 .Should()
//                 .NotBeNull();
//         }

//         private Mock<UserManager<User>> PrepareUserManagerMock()
//         {
//             Mock<IUserStore<User>> userStoreMock = new Mock<IUserStore<User>>();
//             Mock<UserManager<User>> userManagerMock = new Mock<UserManager<User>>(
//                 userStoreMock.Object, null, null, null, null, null, null, null, null);
//             userManagerMock.Object.UserValidators.Add(new UserValidator<User>());
//             userManagerMock.Object.PasswordValidators.Add(new PasswordValidator<User>());

//             return userManagerMock;
//         }

//         private static Mock<RoleManager<Role>> PrepareRoleManagerMock()
//         {
//             var roleStoreMock = new Mock<IRoleStore<Role>>();
//             var roleManagerMock = new Mock<RoleManager<Role>>(roleStoreMock.Object, null, null, null, null);

//             return roleManagerMock;
//         }
//     }
// }
