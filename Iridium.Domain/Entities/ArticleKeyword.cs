using System.ComponentModel.DataAnnotations.Schema;
using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class ArticleKeyword : BaseDomainEntity
{
    // Properties
    public long ArticleId { get; set; }
    public long KeywordId { get; set; }

    // Relationships
    [ForeignKey("ArticleId")]
    public virtual Article Article { get; set; }
    
    [ForeignKey("KeywordId")]
    public virtual Keyword Keyword { get; set; }
}