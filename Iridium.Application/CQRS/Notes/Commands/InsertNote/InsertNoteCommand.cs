using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using MediatR;

namespace Iridium.Application.CQRS.Notes.Commands.InsertNote;

public class InsertNoteCommand : IRequest<ServiceResult<bool>>
{
    public long WorkspaceId { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string Content { get; set; }
    
    public string Summary { get; set; }
    
    public virtual ICollection<NoteKeyword> NoteKeywords { get; set; }
}

public class InsertNoteCommandHandler : IRequestHandler<InsertNoteCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public InsertNoteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(InsertNoteCommand request, CancellationToken cancellationToken)
    {
        var noteEntity = new Note
        {
            WorkspaceId = request.WorkspaceId,
            Title = request.Title,
            Description = request.Description,
            Content = request.Content,
            Summary = request.Summary,
            NoteKeywords = request.NoteKeywords,
        };

        noteEntity.AddDomainEvent(new NoteInsertedEvent(noteEntity));

        _context.Note.Add(noteEntity);

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}
