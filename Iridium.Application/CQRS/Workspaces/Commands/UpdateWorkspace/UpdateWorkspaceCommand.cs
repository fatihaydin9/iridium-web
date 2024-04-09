using Iridium.Domain.Entities;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Categories.Commands.UpdateWorkspace;

public record UpdateWorkspaceCommand : IRequest
{
    public long Id { get; }

    public string Name { get; }

    public string? Note { get; }
    public bool IsPublic { get; }
}

public class UpdateWorkspaceCommandHandler : IRequestHandler<UpdateWorkspaceCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Workspace.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Workspace), request.Id);

        entity.Name = request.Name;
        entity.IsPublic = request.IsPublic;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
