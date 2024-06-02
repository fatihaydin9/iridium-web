using Iridium.Core.Constants;
using Iridium.Core.Models;
using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Iridium.Infrastructure.Services;

public class BaseService
{
    protected readonly AppSettings AppSettings;
    protected readonly IMemoryCache MemoryCache;
    protected readonly ApplicationDbContext DbContext;

    protected BaseService(IMemoryCache memoryCache, ApplicationDbContext dbContext, IOptions<AppSettings> appSettings)
    {
        MemoryCache = memoryCache;
        DbContext = dbContext;
        AppSettings = appSettings.Value;
    }

    public ServiceResult<bool> GetSucceededResult()
    {
        var serviceResult = new ServiceResult<bool>();
        serviceResult.Data = true;
        serviceResult.Succeeded = true;
        serviceResult.Message = "Operation Completed successfully.";
        return serviceResult;
    }

    public ServiceResult<bool> GetFailedResult(string message)
    {
        var serviceResult = new ServiceResult<bool>();
        serviceResult.Data = false;
        serviceResult.Succeeded = false;
        serviceResult.Message = message;
        return serviceResult;
    }

    protected async Task<ServiceResult<List<Role>>> GetRolesFromCacheAsync(bool reloadData = false)
    {
        var cacheKey = CacheKeys.RoleCacheKey;

        if (reloadData)
            MemoryCache.Remove(cacheKey);

        if (MemoryCache.TryGetValue(cacheKey, out List<Role> cachedData))
            return new ServiceResult<List<Role>>(cachedData);

        cachedData = await FetchRolesFromDbAsync();

        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1),
            SlidingExpiration = TimeSpan.FromSeconds(1)
        };

        MemoryCache.Set(cacheKey, cachedData, cacheEntryOptions);

        return new ServiceResult<List<Role>>(cachedData);
    }

    #region Private Methods

    private async Task<List<Role>> FetchRolesFromDbAsync() =>
        await DbContext.Role.Where(w => w.Deleted != true).ToListAsync();

    private async Task<List<string>> FetchRoleParamCodesFromDbAsync() =>
        await DbContext.Role.Where(w => w.Deleted != true).Select(s => s.ParamCode).ToListAsync();

    #endregion
    
    public async Task<List<Role>> GetRoleHierarchyAsync(List<string> paramCodes)
    {
        var result = new List<Role>();
        var rolesFromCache = await GetRolesFromCacheAsync();

        if (!rolesFromCache.Succeeded || rolesFromCache == null || !rolesFromCache.Data.Any())
            return new List<Role>();

        var roles = rolesFromCache.Data;
        var roleQueue = new Queue<Role>(roles.Where(r => paramCodes.Contains(r.ParamCode)));

        while (roleQueue.Count > 0)
        {
            var currentRole = roleQueue.Dequeue();
            result.Add(currentRole);
            var childRoles = roles.Where(r => r.ParentRoleId == currentRole.Id);
            
            foreach (var child in childRoles)
                roleQueue.Enqueue(child);
        }

        return result;
    }
}