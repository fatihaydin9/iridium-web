using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class PasswordUpdatedEvent : BaseEvent
{
    public PasswordUpdatedEvent(Password password)
    {
        Password = password;
    }

    public Password Password { get; }
}