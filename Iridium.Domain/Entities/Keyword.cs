using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Keyword : BaseDomainEntity
{
    // Constructor
    public Keyword()
    {
        NoteKeywords = new HashSet<NoteKeyword>();
    }
    
    // Properties
    public long Id { get; set; }
    public string Name { get; set; }

    // Relationships
    public virtual ICollection<NoteKeyword> NoteKeywords { get; set; }
}
