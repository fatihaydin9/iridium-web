using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class NoteUpdatedEvent : BaseEvent
{
    public NoteUpdatedEvent(Note note)
    {
        Note = note;
    }

    public Note Note { get; }
}