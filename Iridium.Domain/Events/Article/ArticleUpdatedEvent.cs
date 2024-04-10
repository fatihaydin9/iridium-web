using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class ArticleUpdatedEvent : BaseEvent
{
    public ArticleUpdatedEvent(Article article)
    {
        Article = article;
    }

    public Article Article { get; }
}