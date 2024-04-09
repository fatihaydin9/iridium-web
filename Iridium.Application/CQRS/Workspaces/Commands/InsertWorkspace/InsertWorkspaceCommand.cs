using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using MediatR;

namespace Iridium.Application.CQRS.Categories.Commands.InsertWorkspace;

public class InsertWorkspaceCommand : IRequest<long>
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

public class InsertWorkspaceCommandHandler : IRequestHandler<InsertWorkspaceCommand, long>
{
    private readonly IApplicationDbContext _context;

    public InsertWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(InsertWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var entity = new Workspace
        {
            Name = request.Name,
            IsPublic = request.IsPublic
        };

        entity.AddDomainEvent(new WorkspaceInsertedEvent(entity));

        await _context.Workspace.AddAsync(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
