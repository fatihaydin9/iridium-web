using System.Text;
using Iridium.Application.Services.AuthSrv;
using Iridium.Application.Services.RoleSrv;
using Iridium.Core.Auth;
using Iridium.Core.Enums;
using Iridium.Core.Models;
using Iridium.Infrastructure;
using Iridium.Infrastructure.Initializers;
using Iridium.Persistence.Contexts;
using Iridium.Persistence.Interceptors;
using Iridium.Web.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Iridium.Web;

public class Program
{
    public static void Main(string[] args)
    {
        #region Services

        var builder = WebApplication.CreateBuilder(args);
        var jwtSecretKey = builder.Configuration["JwtConfig:SecretKey"];
        var jwtKey = Encoding.ASCII.GetBytes(jwtSecretKey);
        var services = builder.Services;

        // TODO : Cors Policy will be change
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        services.AddHttpContextAccessor();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
        services.AddScoped<EntitySaveChangesInterceptor>();

        var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(connectionString);
            var interceptor = serviceProvider.GetRequiredService<EntitySaveChangesInterceptor>();
            options.AddInterceptors(interceptor);
        });


        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // for development env
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();

        // Configure Application Services
        services.AddApplicationServices();

        // Set Configurations
        services.Configure<AppSettings>(builder.Configuration);
        services.AddSingleton<IConfiguration>(builder.Configuration);
        services.AddSingleton<ConfigurationManager>();

        #endregion

        #region Application

        var app = builder.Build();

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"))
            .Options;

        app.UseMiddleware<ErrorHandlerMiddleware>();

        var serviceProvider = services.BuildServiceProvider();
        var authService = serviceProvider.GetService<IAuthenticatedUser>();
        var dbContext = new ApplicationDbContext(dbContextOptions, authService);
        var roleInitializer = new RoleInitializer(dbContext);
        roleInitializer.InitializeRoles();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.MapControllers();

        app.UseAuthentication();
        app.UseAuthorization();

        LogInitializer.Initialize(ServiceType.Web, builder.Configuration.GetConnectionString("ApplicationDbContext"));

        app.Run();

        #endregion
    }
}