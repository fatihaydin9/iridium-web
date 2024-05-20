using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Articles.EventHandlers;

public class TodoUpdatedEventHandler : INotificationHandler<TodoUpdatedEvent>
{
    private readonly ILogger<TodoUpdatedEventHandler> _logger;

    public TodoUpdatedEventHandler(ILogger<TodoUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}