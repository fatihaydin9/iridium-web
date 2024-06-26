using Iridium.Domain.Entities;

namespace Iridium.Application.Services.RoleSrv;

public interface IRoleService
{
    Task<List<Role>> GetRoleHierarchyAsync(List<string> paramCodes);
}