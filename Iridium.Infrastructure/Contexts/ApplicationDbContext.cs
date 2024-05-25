using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Reflection;
using Iridium.Infrastructure.Interceptors;
using Iridium.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Iridium.Infrastructure.Contexts;

public class ApplicationDbContext : DbContext
{
    private readonly IUserService _authenticatedUserService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IUserService authenticatedUserService)
        : base(options)
    {
        _authenticatedUserService = authenticatedUserService;
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
        var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "../Iridium.Web");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("ApplicationDbContext");

        builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Iridium.Infrastructure"));

        var userService = new MockUserService();
        return new ApplicationDbContext(builder.Options, userService);
    }
}