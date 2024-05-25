using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Infrastructure.Contexts;

public interface IApplicationDbContext
{
    public DbSet<Log> Log { get; }
    public DbSet<User> User { get; }
    public DbSet<Role> Role { get; }
    public DbSet<UserRole> UserRole { get;}
    public DbSet<AuditLog> AuditLog { get; }
    public DbSet<Todo> Todo { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}