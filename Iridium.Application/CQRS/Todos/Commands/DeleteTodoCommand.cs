using Iridium.Core.Exceptions;
using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events.TodoEvent;
using Iridium.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Todos.Commands;

public record DeleteTodoCommand(long Id) : IRequest<ServiceResult<bool>>;

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, ServiceResult<bool>>
{
    private readonly ApplicationDbContext _context;

    public DeleteTodoCommandHandler(ApplicationDbContext context)
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