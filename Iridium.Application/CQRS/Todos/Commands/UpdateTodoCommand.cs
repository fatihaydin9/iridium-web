using MediatR;
using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Core.Exceptions;
using Iridium.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Todos.Commands;

public record UpdateTodoCommand : IRequest<ServiceResult<bool>>
{
    public long Id { get; }
    public string Content { get; }
    public bool IsCompleted { get; }
}

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, ServiceResult<bool>>
{
    private readonly ApplicationDbContext _context;

    public UpdateTodoCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo =
            await _context.Todo.FirstOrDefaultAsync(w => w.Id == request.Id && w.Deleted != true, cancellationToken);

        if (todo == null)
            throw new NotFoundException(nameof(Todo), request.Id);

        todo.UpdateContent(request.Content);
        todo.UpdateIsCompleted(request.IsCompleted);
       
        _context.Todo.Update(todo);

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}