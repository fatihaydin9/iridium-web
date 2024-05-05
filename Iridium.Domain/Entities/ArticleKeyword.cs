using System.ComponentModel.DataAnnotations.Schema;
using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class ArticleKeyword : BaseDomainEntity
{
    public long ArticleId { get; set; }
    
    public long KeywordId { get; set; }

    #region Navigation Properties

    [ForeignKey("ArticleId")]
    public virtual Article Article { get; set; }
    
    [ForeignKey("KeywordId")]
    public virtual Keyword Keyword { get; set; }

    #endregion
    
}