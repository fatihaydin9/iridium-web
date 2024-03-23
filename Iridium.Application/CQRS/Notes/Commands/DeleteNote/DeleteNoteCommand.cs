using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Notes.Commands.DeleteNote;

public record DeleteNoteCommand(long Id) : IRequest<ServiceResult<bool>>;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteNoteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Note.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Note), request.Id);

        _context.Note.Remove(entity);

        entity.AddDomainEvent(new NoteDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}