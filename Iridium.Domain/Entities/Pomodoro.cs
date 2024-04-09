using System.ComponentModel.DataAnnotations.Schema;

namespace Iridium.Domain.Entities;

public class Pomodoro
{
    // Properties
    public long Id { get; set; }
    
    public long WorkspaceId { get; set; }
    
    public short Step { get; set; }
    
    public short Nap { get; set; }
    
    public bool IsFinished { get; set; }

    // Relationships
    [ForeignKey("WorkspaceId")]
    public virtual Workspace Workspace { get; set; }
}
