using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Passwords.EventHandlers;

public class PasswordInsertedEventHandler : INotificationHandler<PasswordInsertedEvent>
{
    private readonly ILogger<PasswordInsertedEventHandler> _logger;

    public PasswordInsertedEventHandler(ILogger<PasswordInsertedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PasswordInsertedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}