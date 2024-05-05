using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Workspaces.Commands;

public record UpdateWorkspaceCommand : IRequest<ServiceResult<bool>>
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string? Note { get; set; }
    public bool IsPublic { get; set; }
}

public class UpdateWorkspaceCommandHandler : IRequestHandler<UpdateWorkspaceCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public UpdateWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Workspace.FindAsync(request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Workspace), request.Id);

        entity.Name = request.Name;
        entity.IsPublic = request.IsPublic;

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}
