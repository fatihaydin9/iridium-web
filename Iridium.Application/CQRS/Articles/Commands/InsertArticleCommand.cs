using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using MediatR;

namespace Iridium.Application.CQRS.Articles.Commands;

public class InsertArticleCommand : IRequest<ServiceResult<bool>>
{
    public long WorkspaceId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public string Summary { get; set; }
    
    public virtual ICollection<ArticleKeyword> ArticleKeywords { get; set; }
}

public class InsertArticleCommandHandler : IRequestHandler<InsertArticleCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public InsertArticleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(InsertArticleCommand request, CancellationToken cancellationToken)
    {
        var noteEntity = new Article()
        {
            WorkspaceId = request.WorkspaceId,
            Title = request.Title,
            Description = request.Description,
            Content = request.Content,
            Summary = request.Summary,
            ArticleKeywords = request.ArticleKeywords
        };

        noteEntity.AddDomainEvent(new ArticleInsertedEvent(noteEntity));

        await _context.Article.AddAsync(noteEntity);

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}
