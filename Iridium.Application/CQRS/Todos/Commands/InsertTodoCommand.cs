using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Persistence.Contexts;
using MediatR;

namespace Iridium.Application.CQRS.Todos.Commands;

public class InsertTodoCommand : IRequest<ServiceResult<bool>>
{
    public string Content { get; set; }
}

public class InsertTodoCommandHandler : IRequestHandler<InsertTodoCommand, ServiceResult<bool>>
{
    private readonly ApplicationDbContext _context;

    public InsertTodoCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(InsertTodoCommand request, CancellationToken cancellationToken)
    {
        var noteEntity = new Todo
        {
            Content = request.Content,
            IsCompleted = false
        };

        noteEntity.AddDomainEvent(new TodoInsertedEvent(noteEntity));

        await _context.Todo.AddAsync(noteEntity);

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}