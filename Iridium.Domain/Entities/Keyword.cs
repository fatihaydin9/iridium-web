using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Keyword : BaseDomainEntity
{
    public Keyword()
    {
        ArticleKeywords = new HashSet<ArticleKeyword>();
    }
    
    public string Name { get; set; }

    #region Navigation Properties

    public virtual ICollection<ArticleKeyword> ArticleKeywords { get; set; }

    #endregion
}
