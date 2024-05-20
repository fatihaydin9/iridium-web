using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class TodoInsertedEvent : BaseEvent
{
    public TodoInsertedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}