using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Workspace : BaseDomainEntity
{
    // Constructor
    public Workspace()
    {
        Articles = new HashSet<Article>();
        Pomodoros = new HashSet<Pomodoro>();
        MindMaps = new HashSet<MindMap>();
    }
    
    // Properties
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsPublic { get; set; }

    // Relationships
    public virtual ICollection<Article> Articles { get; set; }
    public virtual ICollection<Pomodoro> Pomodoros { get; set; }
    public virtual ICollection<MindMap> MindMaps { get; set; }
    
}