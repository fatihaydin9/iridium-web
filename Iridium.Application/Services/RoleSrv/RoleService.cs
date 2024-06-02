using Iridium.Core.Models;
using Iridium.Persistence.Contexts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Iridium.Application.Services.RoleSrv;

public class RoleService : BaseService, IRoleService
{
    public RoleService(IMemoryCache memoryCache, ApplicationDbContext dbContext, IOptions<AppSettings> appSettings) :
        base(memoryCache, dbContext, appSettings)
    {
    }
}