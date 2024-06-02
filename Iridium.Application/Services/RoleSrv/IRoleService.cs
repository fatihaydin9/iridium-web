using Iridium.Domain.Entities;

namespace Iridium.Infrastructure.Services.RoleSrv;

public interface IRoleService
{
    Task<List<Role>> GetRoleHierarchyAsync(List<string> paramCodes);
}