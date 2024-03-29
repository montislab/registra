using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RegistraWebApi.Constants;
using RegistraWebApi.Controllers;
using RegistraWebApi.Models;
using RegistraWebApi.Persistance;
using RegistraWebApi.Persistance.Repository;
using RegistraWebApi.Services;

namespace RegistraWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureIdentityServices(services);
            ConfigureUserAccessServices(services);

            services.AddAutoMapper(typeof(AuthController).Assembly);
            services.AddDbContext<RegistraDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(AppSettingsNodes.RegistraConnectionStringDev)));

            ConfigureDiServices(services);

            services.AddControllers();
            ConfigureDefaultControllerAuthentication(services);
            services.AddCors();
        }

        private void ConfigureDefaultControllerAuthentication(IServiceCollection services)
        {
            //TODO: Check this part of code after finish Identity
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        private void ConfigureDiServices(IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();
        }

        private void ConfigureUserAccessServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection(AppSettingsNodes.JwtNode).Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.RequireAdminRole, policy => policy.RequireRole(RoleNames.Admin));
                options.AddPolicy(PolicyNames.ViewClientData, policy => policy.RequireRole(RoleNames.Admin, RoleNames.Client));
            });
        }

        private void ConfigureIdentityServices(IServiceCollection services)
        {
            //TODO: Password should be more complex
            IdentityBuilder builder = services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<RegistraDbContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //TODO line bottom has to be changed before publish because of security reasons.
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
