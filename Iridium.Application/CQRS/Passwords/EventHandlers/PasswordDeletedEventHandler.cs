using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Passwords.EventHandlers;

public class PasswordDeletedEventHandler : INotificationHandler<PasswordDeletedEvent>
{
    private readonly ILogger<PasswordDeletedEventHandler> _logger;

    public PasswordDeletedEventHandler(ILogger<PasswordDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PasswordDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}