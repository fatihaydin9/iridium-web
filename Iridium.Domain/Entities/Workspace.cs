using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Workspace : BaseDomainEntity
{
    // Constructor
    public Workspace()
    {
        Articles = new HashSet<Article>();
        Pomodoros = new HashSet<Pomodoro>();
    }
    
    // Properties
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsPublic { get; set; }

    // Relationships
    public virtual ICollection<Article> Articles { get; set; }
    public virtual ICollection<Pomodoro> Pomodoros { get; set; }
    
}