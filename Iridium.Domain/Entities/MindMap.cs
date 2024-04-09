using System.ComponentModel.DataAnnotations.Schema;
using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class MindMap : BaseDomainEntity
{
    // Constructor
    public MindMap()
    {
        MindMapItems = new List<MindMapItem>();
    }
    
    // Properties
    public long Id { get; set; }
    public long WorkspaceId { get; set; }
    public string Name { get; set; }

    // Relationships
    [ForeignKey("WorkspaceId")]
    public virtual Workspace Workspace { get; set; }
    
    public virtual ICollection<MindMapItem> MindMapItems { get; set; }
}