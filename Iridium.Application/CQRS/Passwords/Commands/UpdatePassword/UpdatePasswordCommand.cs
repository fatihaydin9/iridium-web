using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Passwords.Commands.UpdatePassword;

public record UpdatePasswordCommand : IRequest<ServiceResult<bool>>
{
    public long Id { get; init; }

    public long CategoryId { get; set; }

    public string Username { get; set; }

    public string Hash { get; set; }

    public string Salt { get; set; }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdatePasswordCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Password
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Category), request.Id);

        entity.CategoryId = request.CategoryId;
        entity.Username = request.Username;
        entity.Hash = request.Hash;
        entity.Salt = request.Salt;

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}
