using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class PasswordInsertedEvent : BaseEvent
{
    public PasswordInsertedEvent(Password password)
    {
        Password = password;
    }

    public Password Password { get; }
}