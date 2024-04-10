using System.ComponentModel.DataAnnotations.Schema;

namespace Iridium.Domain.Entities;

public class Tag
{
    public long Id { get; set; }
    public long ArticleId { get; set; }
    public string Label { get; set; }
    
    [ForeignKey("ArticleId")]
    public virtual Article Article { get; set; }
}