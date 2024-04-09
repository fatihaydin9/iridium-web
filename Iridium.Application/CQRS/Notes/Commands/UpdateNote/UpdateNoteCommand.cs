using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Notes.Commands.UpdateNote;

public record UpdateNoteCommand : IRequest<ServiceResult<bool>>
{
    public long Id { get; set; }

    public long WorkspaceId { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string Content { get; set; }
    
    public string Summary { get; set; }
    
    public virtual ICollection<NoteKeyword> NoteKeywords { get; set; }
}

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public UpdateNoteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var noteEntity = await _context.Note
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (noteEntity == null)
            throw new NotFoundException(nameof(Workspace), request.Id);

        noteEntity = new Note()
        {
            Id = request.Id,
            WorkspaceId = request.WorkspaceId,
            Title = request.Title,
            Description = request.Description,
            Content = request.Content,
            Summary = request.Summary,
            NoteKeywords = request.NoteKeywords,
        };

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}
