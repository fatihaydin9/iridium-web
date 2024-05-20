using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Infrastructure.Contexts;

public interface IApplicationDbContext
{
    DbSet<Todo> Todo { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}