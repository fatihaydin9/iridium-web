using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class TodoDeletedEvent : BaseEvent
{
    public TodoDeletedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}