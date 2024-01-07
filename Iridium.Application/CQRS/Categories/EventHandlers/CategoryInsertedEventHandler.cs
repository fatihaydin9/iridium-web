using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Categories.EventHandlers;

public class CategoryInsertedEventHandler : INotificationHandler<CategoryInsertedEvent>
{
    private readonly ILogger<CategoryInsertedEventHandler> _logger;

    public CategoryInsertedEventHandler(ILogger<CategoryInsertedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CategoryInsertedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
