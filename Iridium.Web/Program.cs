using Iridium.Infrastructure;
using Iridium.Infrastructure.Constants;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Initializers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Iridium.Web;

public class Program
{
    public static void Main(string[] args)
    {
        #region Services

        var builder = WebApplication.CreateBuilder(args);
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

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"), b => b.MigrationsAssembly("Iridium.Infrastructure")));

        var authKey = Encoding.ASCII.GetBytes(SymmetricKey.Value);
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
                IssuerSigningKey = new SymmetricSecurityKey(authKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddApplicationServices();

        #endregion

        #region Application

        var app = builder.Build();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"))
            .Options;

        var dbContext = new ApplicationDbContext(options);
        var roleInitializer = new RoleInitializer(dbContext);
        roleInitializer.InitializeRoles();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        app.UseAuthorization();

        LogInitializer.Initialize(Domain.Enums.ServiceType.Web, builder.Configuration.GetConnectionString("ApplicationDbContext"));

        app.MapControllers().RequireAuthorization();

        app.Run();

        #endregion
    }
}