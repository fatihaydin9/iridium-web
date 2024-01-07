using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class CategoryUpdatedEvent : BaseEvent
{
    public CategoryUpdatedEvent(Category category)
    {
        Category = category;
    }

    public Category Category { get; }
}