using IdentityModel;
using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Iridium.Infrastructure.Interceptors;

public class EntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IUserService _userService;

    public EntitySaveChangesInterceptor(IUserService userService)
    {
        _userService = userService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext context)
    {
        if (context == null) return;

        var timestamp = DateTime.UtcNow;
        var auditEntries = new List<AuditLog>();

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is AuditLog)
                continue;

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified ||
                entry.HasChangedOwnedEntities())
            {
                // Set created and modified timestamps
                if (entry.State == EntityState.Added)
                {
                    entry.CurrentValues["CreatedBy"] = _userService.UserId;
                    entry.CurrentValues["CreatedDate"] = timestamp;
                }

                var entityId = "0";
                if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues["ModifiedBy"] = _userService.UserId;
                    entry.CurrentValues["ModifiedDate"] = timestamp;
                    entityId = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue.ToString();
                }

                // Prepare audit log entry
                var auditLog = new AuditLog()
                {
                    EntityId = entityId,
                    EntityName = entry.Entity.GetType().Name,
                    OldValue = entry.State == EntityState.Added
                        ? null
                        : JsonConvert.SerializeObject(entry.OriginalValues.ToObject()),
                    NewValue = entry.State == EntityState.Deleted
                        ? null
                        : JsonConvert.SerializeObject(entry.CurrentValues.ToObject()),
                    Type = (short)entry.State,
                    Timestamp = timestamp,
                    UserId = _userService.UserId
                };
                auditEntries.Add(auditLog);
            }
        }

        foreach (var audit in auditEntries)
        {
            context.Add(audit);
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
