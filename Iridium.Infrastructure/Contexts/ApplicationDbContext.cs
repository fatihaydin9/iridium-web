using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;

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
    public DbSet<Workspace> Workspace { get; set; }
    public DbSet<Note> Note { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_existingConnection != null)
            optionsBuilder.UseSqlServer(_existingConnection, b => b.MigrationsAssembly("Iridium.Infrastructure"));
        
        else if (!string.IsNullOrEmpty(_connectionString))
            optionsBuilder.UseSqlServer(_connectionString, b => b.MigrationsAssembly("Iridium.Infrastructure"));

        optionsBuilder.LogTo(Console.WriteLine);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "../Iridium.Web");
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("ApplicationDbContext");

        builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Iridium.Infrastructure"));

        return new ApplicationDbContext(builder.Options);
    }
}