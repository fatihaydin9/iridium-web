using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using MediatR;

namespace Iridium.Application.CQRS.Passwords.Commands.InsertPassword;

public class InsertPasswordCommand : IRequest<ServiceResult<bool>>
{
    public long CategoryId { get; init; }

    public string Username { get; init; }

    public string Hash { get; init; }

    public string Salt { get; init; }
}

public class InsertPasswordCommandHandler : IRequestHandler<InsertPasswordCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public InsertPasswordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(InsertPasswordCommand request, CancellationToken cancellationToken)
    {
        var entity = new Password
        {
            Hash = request.Hash,
            Salt = request.Salt,
            Username = request.Username,
            CategoryId = request.CategoryId
        };

        entity.AddDomainEvent(new PasswordInsertedEvent(entity));

        _context.Password.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}
