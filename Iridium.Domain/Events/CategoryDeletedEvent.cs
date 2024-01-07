using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class CategoryDeletedEvent : BaseEvent
{
    public CategoryDeletedEvent(Category category)
    {
        Category = category;
    }

    public Category Category { get; }
}