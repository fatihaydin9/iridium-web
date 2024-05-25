using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Iridium.Infrastructure.Initializers;

public static class AutoMapperInitializer
{
    public static IServiceCollection InitializeAutoMapper(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var targetAssemblies = assemblies.Where(a => a.FullName != null && a.FullName.StartsWith("Iridium.Application"));

        var config = new MapperConfiguration(cfg =>
        {
            foreach (var assembly in targetAssemblies)
            {
                var profileTypes = assembly.GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(Profile)))
                    .ToList();

                foreach (var type in profileTypes)
                {
                    cfg.AddProfile(Activator.CreateInstance(type) as Profile);
                }
            }
        });

        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }

}