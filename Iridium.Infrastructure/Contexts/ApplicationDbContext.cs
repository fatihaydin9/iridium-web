using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Reflection;

namespace Iridium.Infrastructure.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }
    
    private readonly string _connectionString;
    private readonly DbConnection _existingConnection;

    public ApplicationDbContext(string connectionString) => _connectionString = connectionString;
    public ApplicationDbContext(DbConnection existingConnection) => _existingConnection = existingConnection;
    
    public DbSet<Log> Log { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Password> Password { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_existingConnection != null)
            optionsBuilder.UseSqlServer(_existingConnection);
        
        else if (!string.IsNullOrEmpty(_connectionString))
            optionsBuilder.UseSqlServer(_connectionString);

        optionsBuilder.LogTo(Console.WriteLine);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
