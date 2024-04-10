using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Workspaces.Commands;

public record DeleteWorkspaceCommand(long Id) : IRequest<ServiceResult<bool>>;

public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Workspace.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Workspace), request.Id);

        _context.Workspace.Remove(entity);

        entity.AddDomainEvent(new WorkspaceDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}