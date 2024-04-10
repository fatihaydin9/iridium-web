using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Workspaces.EventHandlers;

public class WorkspaceUpdatedEventHandler : INotificationHandler<WorkspaceUpdatedEvent>
{
    private readonly ILogger<WorkspaceUpdatedEventHandler> _logger;

    public WorkspaceUpdatedEventHandler(ILogger<WorkspaceUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(WorkspaceUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}