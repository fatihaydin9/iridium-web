using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Articles.Commands;

public record UpdateArticleCommand : IRequest<ServiceResult<bool>>
{
    public long Id { get; }

    public long WorkspaceId { get; }
    
    public string Title { get; }
    
    public string Description { get; }
    
    public string Content { get; }
    
    public string Summary { get; }
    
    public virtual ICollection<ArticleKeyword> ArticleKeywords { get; set; }
}

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public UpdateArticleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var noteEntity = await _context.Article
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (noteEntity == null)
            throw new NotFoundException(nameof(Workspace), request.Id);

        noteEntity = new Article()
        {
            Id = request.Id,
            WorkspaceId = request.WorkspaceId,
            Title = request.Title,
            Description = request.Description,
            Content = request.Content,
            Summary = request.Summary,
            ArticleKeywords = request.ArticleKeywords,
        };

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}
