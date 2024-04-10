using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Articles.EventHandlers;

public class ArticleInsertedEventHandler : INotificationHandler<ArticleInsertedEvent>
{
    private readonly ILogger<ArticleInsertedEventHandler> _logger;

    public ArticleInsertedEventHandler(ILogger<ArticleInsertedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ArticleInsertedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}