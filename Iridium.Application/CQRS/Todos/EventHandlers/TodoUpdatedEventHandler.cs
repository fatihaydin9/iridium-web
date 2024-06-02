using MediatR;
using Microsoft.Extensions.Logging;
using Iridium.Domain.Events.TodoEvent;

namespace Iridium.Application.CQRS.Todos.EventHandlers;

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