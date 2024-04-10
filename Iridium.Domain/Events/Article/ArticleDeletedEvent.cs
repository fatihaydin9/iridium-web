using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class ArticleDeletedEvent : BaseEvent
{
    public ArticleDeletedEvent(Article article)
    {
        Article = article;
    }

    public Article Article { get; }
}