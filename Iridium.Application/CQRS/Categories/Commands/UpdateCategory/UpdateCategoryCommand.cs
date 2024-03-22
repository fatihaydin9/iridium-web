using Iridium.Domain.Entities;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand : IRequest
{
    public long Id { get; }

    public string Name { get; }

    public string? Note { get; }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Category.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Category), request.Id);

        entity.Name = request.Name;
        entity.Note = request.Note;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
