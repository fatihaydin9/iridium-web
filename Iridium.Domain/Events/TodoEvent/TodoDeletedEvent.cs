using Iridium.Domain.Common;

namespace Iridium.Domain.Events.TodoEvent;

public class TodoDeletedEvent : BaseEvent
{
    public TodoDeletedEvent(Entities.Todo todo)
    {
        Todo = todo;
    }

    public Entities.Todo Todo { get; }
}