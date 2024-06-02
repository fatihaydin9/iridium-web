using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Iridium.Infrastructure.Initializers;

public static class MediatrInitializer
{
    public static IServiceCollection InitializeMediatR(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var targetAssemblies =
            assemblies.Where(a => a.FullName != null && a.FullName.StartsWith("Iridium.Application"));

        services.AddMediatR(cfg =>
        {
            foreach (var assembly in targetAssemblies)
            {
                cfg.RegisterServicesFromAssembly(assembly);

                var notificationHandlerTypes = assembly.GetTypes()
                    .Where(t => t.GetInterfaces().Any(i =>
                        (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotificationHandler<>)) ||
                        (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))
                    ))
                    .ToList();

                foreach (var type in notificationHandlerTypes)
                foreach (var mediatrInterface in type.GetInterfaces())
                    if ((mediatrInterface.IsGenericType &&
                         mediatrInterface.GetGenericTypeDefinition() == typeof(INotificationHandler<>)) ||
                        mediatrInterface.GetGenericTypeDefinition() == typeof(IRequestHandler<>)
                       )
                        services.AddTransient(mediatrInterface, type);
            }
        });

        return services;
    }
}