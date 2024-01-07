using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class PasswordDeletedEvent : BaseEvent
{
    public PasswordDeletedEvent(Password password)
    {
        Password = password;
    }

    public Password Password { get; }
}