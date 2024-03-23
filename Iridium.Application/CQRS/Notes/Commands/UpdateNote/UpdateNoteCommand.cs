using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Notes.Commands.UpdateNote;

public record UpdateNoteCommand : IRequest<ServiceResult<bool>>
{
    public long Id { get; set; }

    public long CategoryId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string Summary { get; set; }
    
    public List<Tag> Tags { get; set; }
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
        var entity = await _context.Note
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Category), request.Id);

        entity.CategoryId = request.CategoryId;
        entity.Title = request.Title;
        entity.Content = request.Content;
        entity.Summary = request.Summary;
        entity.Tags = request.Tags;

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}
