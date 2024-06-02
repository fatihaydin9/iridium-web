using System.ComponentModel;

namespace Iridium.Infrastructure.Extensions;

public static class EnumExtensions
{
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        if (field != null)
        {
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            if (attribute != null) return attribute.Description;
        }

        return value.ToString();
    }
}