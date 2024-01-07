namespace Iridium.Infrastructure.Attributes;

public class RoleNameAttribute : Attribute
{
    public string Name;

    public RoleNameAttribute(string Name)
    {
        this.Name = Name;
    }
}
