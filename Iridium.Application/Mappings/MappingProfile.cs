using AutoMapper;
using System.Reflection;

namespace Iridium.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count <= 0) continue;
                foreach (var interfaceMethodInfo in interfaces.Select(@interface => @interface.GetMethod(mappingMethodName, argumentTypes)))
                {
                    interfaceMethodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }

        return;

        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
    }
}
