using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Categories.Commands.DeleteWorkspace;

public record DeleteWorkspaceCommand(long Id) : IRequest;

public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Workspace.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Workspace), request.Id);

        _context.Workspace.Remove(entity);

        entity.AddDomainEvent(new WorkspaceDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}