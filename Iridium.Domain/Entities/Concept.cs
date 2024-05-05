using System.ComponentModel.DataAnnotations.Schema;
using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Concept : BaseDomainEntity
{

    public long ArticleId { get; set; }

    public string Name { get; set; }

    #region Navigation Properties

    [ForeignKey("ArticleId")]
    public virtual Article Article { get; set; }

    #endregion
    
}
