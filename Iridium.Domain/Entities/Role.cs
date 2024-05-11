using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Role : BaseEntity
{
    public long? ParentRoleId { get; set; }

    public string Name { get; set; }

    public string ParamCode { get; set; }
}
