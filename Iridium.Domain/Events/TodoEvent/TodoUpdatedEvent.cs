using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events.TodoEvent;

public class TodoUpdatedEvent : BaseEvent
{
    public TodoUpdatedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}