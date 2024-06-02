using System.Reflection;
using Iridium.Core.Attributes;
using Iridium.Core.Cache;
using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Infrastructure.Initializers;

public class RoleInitializer
{
    private readonly ApplicationDbContext _dbContext;

    public RoleInitializer(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InitializeRoles()
    {
        var roleTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IRole).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var type in roleTypes) await EnsureRoles(type);

        await _dbContext.SaveChangesAsync();
    }

    private async Task EnsureRoles(Type roleType)
    {
        var fields = roleType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        var fullRoleField =
            fields.FirstOrDefault(f => f.GetCustomAttribute<RoleNameAttribute>()?.Name.Contains("Full") ?? false);
        var nonFullRoles = fields.Where(f => f != fullRoleField).ToList();

        long? fullRoleId = null;
        if (fullRoleField != null) fullRoleId = await EnsureRole(fullRoleField);

        foreach (var field in nonFullRoles)
            await EnsureRole(field, fullRoleId);
    }

    private async Task<long> EnsureRole(FieldInfo roleField, long? parentRoleId = null)
    {
        var roleNameAttr = roleField.GetCustomAttribute<RoleNameAttribute>();
        if (roleNameAttr == null)
            return 0;

        var role = await _dbContext.Role.FirstOrDefaultAsync(r => r.ParamCode == roleNameAttr.ParamCode);
        if (role == null)
        {
            role = new Role
            {
                Name = roleNameAttr.Name,
                ParamCode = roleNameAttr.ParamCode,
                ParentRoleId = parentRoleId,
                CreatedBy = 1,
                CreatedDate = DateTime.UtcNow
            };

            _dbContext.Role.Add(role);
            await _dbContext.SaveChangesAsync();
        }
        else if (role.Name != roleNameAttr.Name || role.ParentRoleId != parentRoleId)
        {
            role.Name = roleNameAttr.Name;
            role.ParentRoleId = parentRoleId;
            _dbContext.Role.Update(role);
        }

        if (!string.IsNullOrEmpty(role.ParamCode) && role.Id != 0)
            RoleCache.AddOrUpdate(role.ParamCode, role.Id);

        return role.Id;
    }
}