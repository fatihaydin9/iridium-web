using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Area : BaseEntity
{
    public Area()
    {
        Roles = new HashSet<Role>();
    }
    
    public string Name { get; set; }

    public string ParamCode { get; set; }

    #region Navigation Properties

    public virtual ICollection<Role> Roles { get; set; }

    #endregion
    
}
