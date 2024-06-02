using Iridium.Domain.Events.TodoEvent;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Todos.EventHandlers;

public class TodoInsertedEventHandler : INotificationHandler<TodoInsertedEvent>
{
    private readonly ILogger<TodoInsertedEventHandler> _logger;

    public TodoInsertedEventHandler(ILogger<TodoInsertedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoInsertedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}