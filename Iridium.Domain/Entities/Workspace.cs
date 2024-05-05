using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Workspace : BaseDomainEntity
{
    public Workspace()
    {
        Pomodoros = new HashSet<Pomodoro>();
    }

    public string Name { get; set; } = string.Empty;

    public bool IsPublic { get; set; } 

    #region Navigation Properties
    
    public virtual ICollection<Article> Articles { get; set; }
    public virtual ICollection<Pomodoro> Pomodoros { get; set; }

    #endregion
    
}