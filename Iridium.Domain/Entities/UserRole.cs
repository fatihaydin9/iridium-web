using Iridium.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iridium.Domain.Entities;

public class UserRole : BaseEntity
{
    public long UserId { get; set; }
    public long RoleId { get; set; }

    #region Navigation Properties

    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }

    #endregion
    
}