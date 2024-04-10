using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Articles.EventHandlers;

public class ArticleUpdatedEventHandler : INotificationHandler<ArticleUpdatedEvent>
{
    private readonly ILogger<ArticleUpdatedEventHandler> _logger;

    public ArticleUpdatedEventHandler(ILogger<ArticleUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ArticleUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}