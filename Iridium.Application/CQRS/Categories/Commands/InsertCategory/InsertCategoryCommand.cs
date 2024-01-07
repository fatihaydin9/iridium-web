using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using MediatR;

namespace Iridium.Application.CQRS.Categories.Commands.InsertCategory;

public class InsertCategoryCommand : IRequest<long>
{
    public string Name { get; init; }

    public string Note { get; init; }
}

public class InsertCategoryCommandHandler : IRequestHandler<InsertCategoryCommand, long>
{
    private readonly IApplicationDbContext _context;

    public InsertCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(InsertCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Category
        {
            Name = request.Name,
            Note = request.Note,
        };

        entity.AddDomainEvent(new CategoryInsertedEvent(entity));

        _context.Category.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
