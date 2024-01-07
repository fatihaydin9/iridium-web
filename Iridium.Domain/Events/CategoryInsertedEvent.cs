using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class CategoryInsertedEvent : BaseEvent
{
    public CategoryInsertedEvent(Category category)
    {
        Category = category;
    }

    public Category Category { get; }
}