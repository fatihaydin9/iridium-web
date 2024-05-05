using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Role : BaseEntity
{
    public long AreaId { get; set; }
    
    public long? ParentRoleId { get; set; }

    public string Name { get; set; }

    public string ParamCode { get; set; }
}
