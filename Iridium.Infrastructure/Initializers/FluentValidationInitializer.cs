namespace Iridium.Infrastructure.Initializers;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

public static class FluentValidationInitializer
{
    public static IServiceCollection InitializeFluentValidators(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var targetAssemblies = assemblies.Where(a => a.FullName.StartsWith("Iridium.Application"));

        foreach (var assembly in targetAssemblies)
        {
            var validatorTypes = assembly.GetTypes()
                .Where(t => t.BaseType != null &&
                            t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
                .ToList();

            foreach (var type in validatorTypes)
            {
                var validatorInterface = type.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && 
                                         i.GetGenericTypeDefinition() == typeof(IValidator<>));
                if (validatorInterface != null)
                {
                    services.AddTransient(validatorInterface, type);
                }
            }
        }

        return services;
    }
}
