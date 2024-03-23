using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Notes.EventHandlers;

public class NoteDeletedEventHandler : INotificationHandler<NoteDeletedEvent>
{
    private readonly ILogger<NoteDeletedEventHandler> _logger;

    public NoteDeletedEventHandler(ILogger<NoteDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(NoteDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}