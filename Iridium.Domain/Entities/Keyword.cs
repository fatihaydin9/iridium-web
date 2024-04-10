using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Keyword : BaseDomainEntity
{
    // Constructor
    public Keyword()
    {
        ArticleKeywords = new HashSet<ArticleKeyword>();
    }
    
    // Properties
    public long Id { get; set; }
    public string Name { get; set; }

    // Relationships
    public virtual ICollection<ArticleKeyword> ArticleKeywords { get; set; }
}
