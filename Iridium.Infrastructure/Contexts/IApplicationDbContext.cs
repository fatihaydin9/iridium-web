using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Infrastructure.Contexts;

public interface IApplicationDbContext
{
    DbSet<Category> Category { get; }

    DbSet<Password> Password { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}