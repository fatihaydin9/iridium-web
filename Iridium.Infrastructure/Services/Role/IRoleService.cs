using Iridium.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.Services;

public interface IRoleService
{
    Task<List<Role>> GetRoleHierarchyAsync(List<string> paramCodes);
}