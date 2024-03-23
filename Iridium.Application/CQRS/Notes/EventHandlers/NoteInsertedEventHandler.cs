using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Notes.EventHandlers;

public class NoteInsertedEventHandler : INotificationHandler<NoteInsertedEvent>
{
    private readonly ILogger<NoteInsertedEventHandler> _logger;

    public NoteInsertedEventHandler(ILogger<NoteInsertedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(NoteInsertedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}