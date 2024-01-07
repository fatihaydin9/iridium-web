using AutoMapper;
using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Constants;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace Iridium.Web.Controllers.Base;

[ApiController]
[ApiExceptionFilter]
[Route("[controller]")]
public abstract class ApiBaseController : ControllerBase
{
    #region Mediator

    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    #endregion

    protected readonly IMapper _mapper;
    protected readonly IMemoryCache _memoryCache;
    protected readonly ApplicationDbContext _dbContext;
    protected readonly IAuthenticatedUserService _userService;

    protected async Task<ServiceResult<User>> GetActiveUserAsync()
    {
        var activeUserId = _userService.UserId;
        var user = await _dbContext.User.FindAsync(activeUserId);

        if (user == null)
            return new ServiceResult<User>(new User());
        else
            return new ServiceResult<User>(user);
    }

    protected async Task<ServiceResult<List<Role>>> GetRolesFromCacheAsync(bool reloadData = false)
    {
        var cacheKey = CacheKeys.ROLE_CACHE_KEY;

        if (reloadData)
            _memoryCache.Remove(cacheKey);

        if (!_memoryCache.TryGetValue(cacheKey, out List<Role> cachedData))
        {
            cachedData = await FetchRolesFromDbAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1),
                SlidingExpiration = TimeSpan.FromSeconds(1)
            };

            _memoryCache.Set(cacheKey, cachedData, cacheEntryOptions);
        }

        return new ServiceResult<List<Role>>(cachedData);
    }

    #region Private Methods 

    private async Task<List<Role>> FetchRolesFromDbAsync() => await _dbContext.Role.Where(w => w.Deleted != true).ToListAsync();

    private async Task<List<string>> FetchRoleParamCodesFromDbAsync() => await _dbContext.Role.Where(w => w.Deleted != true).Select(s => s.ParamCode).ToListAsync();

    #endregion

}
