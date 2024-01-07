using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Passwords.EventHandlers;

public class PasswordUpdatedEventHandler : INotificationHandler<PasswordUpdatedEvent>
{
    private readonly ILogger<PasswordUpdatedEventHandler> _logger;

    public PasswordUpdatedEventHandler(ILogger<PasswordUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PasswordUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
