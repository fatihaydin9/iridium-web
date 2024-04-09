using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Categories.EventHandlers;

public class WorkspaceDeletedEventHandler : INotificationHandler<WorkspaceDeletedEvent>
{
    private readonly ILogger<WorkspaceDeletedEventHandler> _logger;

    public WorkspaceDeletedEventHandler(ILogger<WorkspaceDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
