using Iridium.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iridium.Domain.Entities;

public class Note : BaseDomainEntity
{
    // Constructor
    public Note()
    {
        NoteKeywords = new HashSet<NoteKeyword>();
    }
    
    // Properties
    public long Id { get; set; }
    
    public long WorkspaceId { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string Content { get; set; }
    
    public string Summary { get; set; }
    
    // Relationships
    [ForeignKey("WorkspaceId")]
    public virtual Workspace Workspace { get; set; }
    public virtual ICollection<NoteKeyword> NoteKeywords { get; set; }
}
