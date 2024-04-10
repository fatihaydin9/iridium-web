using Iridium.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iridium.Application.CQRS.Articles.EventHandlers;

public class ArticleDeletedEventHandler : INotificationHandler<ArticleDeletedEvent>
{
    private readonly ILogger<ArticleDeletedEventHandler> _logger;

    public ArticleDeletedEventHandler(ILogger<ArticleDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ArticleDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iridium Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}