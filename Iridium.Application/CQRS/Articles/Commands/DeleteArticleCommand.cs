using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Articles.Commands;

public record DeleteArticleCommand(long Id) : IRequest<ServiceResult<bool>>;

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteArticleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Article.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Article), request.Id);

        _context.Article.Remove(entity);

        entity.AddDomainEvent(new ArticleDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}