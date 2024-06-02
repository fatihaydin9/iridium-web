using Iridium.Core.Auth;
using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Iridium.Persistence.Interceptors;

public class EntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public EntitySaveChangesInterceptor(IAuthenticatedUser authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context != null) UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context != null) UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext context)
    {
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
                    entry.CurrentValues["CreatedBy"] = _authenticatedUser.UserId;
                    entry.CurrentValues["CreatedDate"] = timestamp;
                }

                var entityId = "0";
                if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues["ModifiedBy"] = _authenticatedUser.UserId;
                    entry.CurrentValues["ModifiedDate"] = timestamp;
                    entityId = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue
                        ?.ToString();
                }

                // Prepare audit log entry
                if (entityId != null)
                {
                    var auditLog = new AuditLog
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
                        UserId = _authenticatedUser.UserId
                    };
                    auditEntries.Add(auditLog);
                }
            }
        }

        foreach (var audit in auditEntries) context.Add(audit);
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}