using Iridium.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iridium.Domain.Entities;

public class Note : BaseDomainEntity
{
    public Note()
    {
        Tags = new HashSet<Tag>();
    }
    
    public long CategoryId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string Summary { get; set; }

    public bool IsPrivate { get; set; } = true;
    
    // Navigation Properties
    public virtual ICollection<Tag> Tags { get; set; }
    
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

}
