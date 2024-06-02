using System.ComponentModel.DataAnnotations.Schema;

namespace Iridium.Domain.Common;

public class BaseDomainEntity : BaseEntity
{
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped] public IEnumerable<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}