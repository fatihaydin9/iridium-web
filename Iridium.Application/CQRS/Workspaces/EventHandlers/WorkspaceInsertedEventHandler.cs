using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Categories.EventHandlers;

public class WorkspaceInsertedEventHandler : INotificationHandler<WorkspaceInsertedEvent>
{
    private readonly ILogger<WorkspaceInsertedEventHandler> _logger;

    public WorkspaceInsertedEventHandler(ILogger<WorkspaceInsertedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(WorkspaceInsertedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
