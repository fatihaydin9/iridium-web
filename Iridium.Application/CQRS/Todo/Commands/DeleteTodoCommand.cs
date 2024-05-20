using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Articles.Commands;

public record DeleteTodoCommand(long Id) : IRequest<ServiceResult<bool>>;

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _context.Todo.FirstOrDefaultAsync(w => w.Id == request.Id && w.Deleted != true, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Todo), request.Id);

        entity.Deleted = true;

        _context.Todo.Update(entity);

        entity.AddDomainEvent(new TodoDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}