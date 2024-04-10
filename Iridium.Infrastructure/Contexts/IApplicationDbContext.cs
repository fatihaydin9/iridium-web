using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Infrastructure.Contexts;

public interface IApplicationDbContext
{
    DbSet<Workspace> Workspace { get; }

    DbSet<Article> Article { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}