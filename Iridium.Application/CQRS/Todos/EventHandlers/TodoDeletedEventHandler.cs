using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Todos.EventHandlers;

public class TodoDeletedEventHandler : INotificationHandler<TodoDeletedEvent>
{
    private readonly ILogger<TodoDeletedEventHandler> _logger;

    public TodoDeletedEventHandler(ILogger<TodoDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}