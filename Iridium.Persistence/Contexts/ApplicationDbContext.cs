using System.Reflection;
using Iridium.Core.Auth;
using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Iridium.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IAuthenticatedUser authenticatedUser)
        : base(options)
    {
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<Log> Log { get; init; }
    public DbSet<User> User { get; init; }
    public DbSet<Role> Role { get; init; }
    public DbSet<UserRole> UserRole { get; init; }
    public DbSet<AuditLog> AuditLog { get; init; }
    public DbSet<Todo> Todo { get; init; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "../Iridium.Web");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("LocalDbContext");

        builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Iridium.Persistence"));

        var userService = new MockUserService();
        return new ApplicationDbContext(builder.Options, userService);
    }
}