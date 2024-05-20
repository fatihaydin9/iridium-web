using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Articles.Commands;

public record UpdateTodoCommand : IRequest<ServiceResult<bool>>
{
    public long Id { get; }
    public string Content { get; }
    public bool IsCompleted { get; }
}

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var noteEntity =
            await _context.Todo.FirstOrDefaultAsync(w => w.Id == request.Id && w.Deleted != true, cancellationToken);

        if (noteEntity == null)
            throw new NotFoundException(nameof(Todo), request.Id);

        noteEntity = new Todo()
        {
            Id = request.Id,
            Content = request.Content,
            IsCompleted = request.IsCompleted,
        };

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}