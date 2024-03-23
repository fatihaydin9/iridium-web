using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Category : BaseDomainEntity
{
    public Category()
    {
        Notes = new HashSet<Note>();
    }

    public string Name { get; set; } 

    public string? Note { get; set; } 

    public virtual ICollection<Note> Notes { get; set; }

}
