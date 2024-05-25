using Iridium.Domain.Entities;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Iridium.Application.Services;

public class RoleService : BaseService, IRoleService
{
    public RoleService(IMemoryCache memoryCache, ApplicationDbContext dbContext, IOptions<AppSettings> appSettings) : base(memoryCache, dbContext, appSettings)
    {
    }
    
    
}