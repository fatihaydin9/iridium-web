using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class TodoUpdatedEvent : BaseEvent
{
    public TodoUpdatedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}