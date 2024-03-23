using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Notes.EventHandlers;

public class NoteUpdatedEventHandler : INotificationHandler<NoteUpdatedEvent>
{
    private readonly ILogger<NoteUpdatedEventHandler> _logger;

    public NoteUpdatedEventHandler(ILogger<NoteUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(NoteUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}