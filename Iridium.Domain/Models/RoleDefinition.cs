namespace Iridium.Domain.Models;

public class RoleDefinition
{
    public string Name { get; set; }
    public string ParamCode { get; set; }

    public RoleDefinition(string name, string paramCode)
    {
        Name = name;
        ParamCode = paramCode;
    }
}