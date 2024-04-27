using FluentValidation;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Initializers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Iridium.Infrastructure.Services;

namespace Iridium.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add fundamentals
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpClient();
        services.AddMemoryCache();
        
        // Add Auth Services
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

        // Add AutoMapper with automatic binding structure
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Add Validations from assembly
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        // Add MediaTR and its behaviour for pipelining
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        // Initialize role structure : add or delete domain roles
        services.AddTransient<RoleInitializer>();

        return services;
    }
}