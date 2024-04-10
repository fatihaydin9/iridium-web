using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using MediatR;

namespace Iridium.Application.CQRS.Workspaces.Commands;

public class InsertWorkspaceCommand : IRequest<ServiceResult<bool>>
{
    public InsertWorkspaceCommand(string? note, string name, bool isPublic)
    {
        Note = note;
        Name = name;
        IsPublic = isPublic;
    }

    public string Name { get; }

    public string? Note { get; }
    
    public bool IsPublic { get; set; }

}

public class InsertWorkspaceCommandHandler : IRequestHandler<InsertWorkspaceCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public InsertWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(InsertWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var entity = new Workspace
        {
            Name = request.Name,
            IsPublic = request.IsPublic
        };

        entity.AddDomainEvent(new WorkspaceInsertedEvent(entity));

        await _context.Workspace.AddAsync(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}
