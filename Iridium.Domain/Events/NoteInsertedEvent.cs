using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class NoteInsertedEvent : BaseEvent
{
    public NoteInsertedEvent(Note note)
    {
        Note = note;
    }

    public Note Note { get; }
}