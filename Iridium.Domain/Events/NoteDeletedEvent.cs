using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class NoteDeletedEvent : BaseEvent
{
    public NoteDeletedEvent(Note note)
    {
        Note = note;
    }

    public Note Note { get; }
}