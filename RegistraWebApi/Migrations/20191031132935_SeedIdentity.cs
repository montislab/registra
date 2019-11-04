using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistraWebApi.Migrations
{
    public partial class SeedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string admin = "Admin";
            const string montisMail = "montislabpoland@gmail.com";

            migrationBuilder.Sql("INSERT [dbo].[AspNetRoles] ([Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'Client', N'CLIENT', N'67ba544f-155c-4a2f-bf0f-b5349e806c16')");
            migrationBuilder.Sql("INSERT [dbo].[AspNetRoles] ([Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'Admin', N'ADMIN', N'31fa05dd-e48f-40d3-9c53-c8c1871e31d5')");            

            migrationBuilder.Sql("INSERT [dbo].[AspNetUsers] ([UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'montislabpoland@gmail.com', N'MONTISLABPOLAND@GMAIL.COM', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEMydwQX600A8lD3xdEJsbn9fLri5GdjTr24JsaJJ3r7U8kSxkPTLJhjNkFF3Yfz1vA==', N'7X465I3JWI6QRH7LN4K2Q6QSAF5RRGGH', N'c56ade14-d5ae-42cc-89b0-1c683ae62d14', NULL, 0, 0, NULL, 1, 0)");
            
            migrationBuilder.Sql(
                @$"INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (
                (select Id from AspNetUsers where UserName = '{montisMail}'),
                (select Id from AspNetRoles where Name = '{admin}'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUserRoles]");
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers]");
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetRoles]");

            migrationBuilder.Sql("DBCC CHECKIDENT('dbo.AspNetRoles', RESEED, 0)");
            migrationBuilder.Sql("DBCC CHECKIDENT('dbo.AspNetUsers', RESEED, 0)");
        }
    }
}
