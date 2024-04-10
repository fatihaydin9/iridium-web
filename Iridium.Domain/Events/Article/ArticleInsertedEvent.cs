using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class ArticleInsertedEvent : BaseEvent
{
    public ArticleInsertedEvent(Article article)
    {
        Article = article;
    }

    public Article Article { get; }
}