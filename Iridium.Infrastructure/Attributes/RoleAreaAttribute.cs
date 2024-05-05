namespace Iridium.Infrastructure.Attributes;

public class RoleAreaAttribute : Attribute
{
    public string Name;
    public string ParamCode;

    public RoleAreaAttribute(string name, string paramCode)
    {
        Name = name;
        ParamCode = paramCode;
    }
}
