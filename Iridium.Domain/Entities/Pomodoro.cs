using System.ComponentModel.DataAnnotations.Schema;
using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Pomodoro : BaseDomainEntity
{
    public long WorkspaceId { get; set; }
    
    public short Step { get; set; }
    
    public short Nap { get; set; }
    
    public bool IsFinished { get; set; }

    #region Navigation Properties

    [ForeignKey("WorkspaceId")]
    public virtual Workspace Workspace { get; set; }

    #endregion
}
