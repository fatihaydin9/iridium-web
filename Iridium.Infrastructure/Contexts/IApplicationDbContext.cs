using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Infrastructure.Contexts;

public interface IApplicationDbContext
{
    DbSet<Workspace> Workspace { get; }

    DbSet<Note> Note { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}