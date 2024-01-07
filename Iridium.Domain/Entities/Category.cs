using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Category : BaseDomainEntity
{
    public Category()
    {
        Passwords = new HashSet<Password>();
    }

    public string Name { get; set; } 

    public string Note { get; set; } 

    public virtual ICollection<Password> Passwords { get; set; }

}
