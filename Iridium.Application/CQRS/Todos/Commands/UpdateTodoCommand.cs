using MediatR;
using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Core.Exceptions;
using Iridium.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Todos.Commands;

public record UpdateTodoCommand : IRequest<ServiceResult<bool>>
{
    public long Id { get; set; }
    public string Content { get; set; }
    public bool IsCompleted { get; set; }
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
        var entity =
            await _context.Todo.FirstOrDefaultAsync(w => w.Id == request.Id && w.Deleted != true, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Todo), request.Id);

        entity.IsCompleted = request.IsCompleted;
        entity.Content = request.Content;
       
        _context.Todo.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}