using System.ComponentModel.DataAnnotations.Schema;
using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class MindMapItem : BaseDomainEntity
{
    // Constructor
    public MindMapItem()
    {
        ChildMindMapItems = new HashSet<MindMapItem>();
    }
    
    // Properties
    public long Id { get; set; }
    public long MindMapId { get; set; }
    public string Concept { get; set; }
    public long? ParentMindMapItemId { get; set; }

    // Relationships
    [ForeignKey("KeywordId")]
    public virtual MindMap MindMap { get; set; }
    
    [ForeignKey("ParentMindMapItemId")]
    public virtual MindMapItem ParentMindMapItem { get; set; }
    
    public virtual ICollection<MindMapItem> ChildMindMapItems { get; set; }
}