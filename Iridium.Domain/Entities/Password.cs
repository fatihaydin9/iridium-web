using Iridium.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iridium.Domain.Entities;

public class Password : BaseDomainEntity
{
    public long CategoryId { get; set; }

    public string Username { get; set; } 

    public string Hash { get; set; } 

    public string Salt { get; set; }


    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}
