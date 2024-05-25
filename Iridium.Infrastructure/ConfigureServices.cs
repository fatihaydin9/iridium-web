using FluentValidation;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Initializers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Iridium.Application.Services;
using Iridium.Infrastructure.Interceptors;
using Iridium.Infrastructure.Services;
using Microsoft.OpenApi.Models;

namespace Iridium.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add fundamentals
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization: Enter JWT token directly in the input below.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });

        // Add Fundamental Services
        services.AddHttpClient();
        services.AddMemoryCache();
        
        // Add Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<EntitySaveChangesInterceptor>();

        // Initializers
        services.InitializeMediatR();
        services.InitializeAutoMapper();
        services.InitializeFluentValidators();
        
        // Initialize role structure : add or delete domain roles
        services.AddTransient<RoleInitializer>();
            
        return services;
    }
}