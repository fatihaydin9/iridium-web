using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using MediatR;

namespace Iridium.Application.CQRS.Notes.Commands.InsertNote;

public class InsertNoteCommand : IRequest<ServiceResult<bool>>
{
    public long CategoryId { get; init; }

    public string Title { get; init; }

    public string Content { get; init; }

    public string Summary { get; init; }

    public bool IsPrivate { get; set; }
    
    public List<Tag> Tags { get; init; }
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
            CategoryId = request.CategoryId,
            Title = request.Title,
            Content = request.Content,
            Summary = request.Summary,
            IsPrivate = request.IsPrivate,
            Tags = request.Tags
        };

        noteEntity.AddDomainEvent(new NoteInsertedEvent(noteEntity));

        _context.Note.Add(noteEntity);

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}
