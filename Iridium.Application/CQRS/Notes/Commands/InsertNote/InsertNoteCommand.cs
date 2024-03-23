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
        var entity = new Note
        {
            CategoryId = request.CategoryId,
            Content = request.Content,
            
        };

        entity.AddDomainEvent(new NoteInsertedEvent(entity));

        _context.Note.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}
