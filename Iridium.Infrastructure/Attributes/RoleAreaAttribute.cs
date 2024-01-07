namespace Iridium.Infrastructure.Attributes;

public class RoleAreaAttribute : Attribute
{
    public string Area;

    public RoleAreaAttribute(string Area)
    {
        this.Area = Area;
    }
}
