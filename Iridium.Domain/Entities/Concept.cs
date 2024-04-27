using System.ComponentModel.DataAnnotations.Schema;
using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Concept : BaseDomainEntity
{
    // Properties
    public long Id { get; set; }
    public long ArticleId { get; set; }
    public string Name { get; set; }
    

    // Relationships
    [ForeignKey("ArticleId")]
    public virtual Article Article { get; set; }
}