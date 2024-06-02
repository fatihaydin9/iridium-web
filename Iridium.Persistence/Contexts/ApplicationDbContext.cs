using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Reflection;
using Iridium.Core.Auth;
using Iridium.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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

    public DbSet<Log> Log { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<AuditLog> AuditLog { get; set; }
    public DbSet<Todo> Todo { get; set; }


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
        var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "../Iridium.WebAPI");
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