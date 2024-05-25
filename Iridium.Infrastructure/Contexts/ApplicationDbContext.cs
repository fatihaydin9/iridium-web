using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Reflection;
using Iridium.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Iridium.Infrastructure.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IUserService _authenticatedUserService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IUserService authenticatedUserService)
        : base(options)
    {
        _authenticatedUserService = authenticatedUserService;
    }

    private readonly string _connectionString;
    private readonly DbConnection _existingConnection;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_existingConnection != null)
            optionsBuilder.UseSqlServer(_existingConnection, b => b.MigrationsAssembly("Iridium.Infrastructure"));

        else if (!string.IsNullOrEmpty(_connectionString))
            optionsBuilder.UseSqlServer(_connectionString, b => b.MigrationsAssembly("Iridium.Infrastructure"));

        optionsBuilder.LogTo(Console.WriteLine);
    }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();

        var timestamp = DateTime.UtcNow;
        var auditEntries = new List<AuditLog>();

        foreach (var entry in ChangeTracker.Entries().Where(e =>
                     e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
        {
            if (entry.Entity is AuditLog)
            {
                continue;
            }

            var auditLog = new AuditLog
            {
                EntityId = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue.ToString(),
                EntityName = entry.Entity.GetType().Name,
                OldValue = entry.State == EntityState.Added
                    ? null
                    : JsonConvert.SerializeObject(entry.OriginalValues.ToObject()),
                NewValue = entry.State == EntityState.Deleted
                    ? null
                    : JsonConvert.SerializeObject(entry.CurrentValues.ToObject()),
                Type = (short)entry.State,
                Timestamp = timestamp,
                UserId = _authenticatedUserService.UserId
            };

            auditEntries.Add(auditLog);
        }

        foreach (var auditEntry in auditEntries)
        {
            AuditLog.Add(auditEntry);
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        var timestamp = DateTime.UtcNow;
        var auditEntries = new List<AuditLog>();

        foreach (var entry in ChangeTracker.Entries().Where(e =>
                     e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
        {
            if (entry.Entity is AuditLog)
            {
                continue;
            }

            var entityName = entry.Entity.GetType().Name;

            var entityId =
                Convert.ToInt64(entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue);

            if (entityId < 0)
                entityId = 0;
            
            var auditLog = new AuditLog()
            {
                EntityId = entityId.ToString(),
                EntityName = entityName,
                OldValue = entry.State == EntityState.Added
                    ? null
                    : JsonConvert.SerializeObject(entry.OriginalValues.ToObject()),
                NewValue = entry.State == EntityState.Deleted
                    ? null
                    : JsonConvert.SerializeObject(entry.CurrentValues.ToObject()),
                Type = (short)entry.State,
                Timestamp = timestamp,
                UserId = _authenticatedUserService.UserId
            };
            
            auditEntries.Add(auditLog);

        }

        auditEntries = auditEntries
            .Where(w => !w.EntityName.ToUpper().Contains("LOG") && w.EntityName.ToUpper() != "ROLE").ToList();
                    
        foreach (var auditEntry in auditEntries)
        {
            AuditLog.Add(auditEntry);
        }

        return await base.SaveChangesAsync(cancellationToken);
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