namespace Iridium.Infrastructure.Attributes;

public class RoleNameAttribute : Attribute
{
    public string Name;
    public string ParamCode;
    
    public RoleNameAttribute(string name, string paramCode)
    {
        Name = name;
        ParamCode = paramCode;
    }
}
