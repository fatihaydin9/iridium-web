using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Events;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Exceptions;
using MediatR;

namespace Iridium.Application.CQRS.Passwords.Commands.DeleteCategory;

public record DeletePasswordCommand(long Id) : IRequest<ServiceResult<bool>>;

public class DeletePasswordCommandHandler : IRequestHandler<DeletePasswordCommand, ServiceResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeletePasswordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<bool>> Handle(DeletePasswordCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Password.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Password), request.Id);

        _context.Password.Remove(entity);

        entity.AddDomainEvent(new PasswordDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);
    }
}