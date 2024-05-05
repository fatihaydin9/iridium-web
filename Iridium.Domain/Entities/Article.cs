using Iridium.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iridium.Domain.Entities;

public class Article : BaseDomainEntity
{
    public Article()
    {
        ArticleKeywords = new HashSet<ArticleKeyword>();
    }
    
    public long WorkspaceId { get; set; }
    
    public string Title { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public string Summary { get; set; }
    

    #region Navigation Properties

    [ForeignKey("WorkspaceId")]
    public virtual Workspace Workspace { get; set; }
    public virtual ICollection<Concept> Concepts { get; set; }
    public virtual ICollection<ArticleKeyword> ArticleKeywords { get; set; }

    #endregion
    
}
