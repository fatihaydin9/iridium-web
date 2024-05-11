using Iridium.Application.Areas;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.Roles;

public class WorkspaceRole : IRole
{
    [RoleName("Workspace Full Role", RoleParamCode.WorkspaceFull)]
    public static readonly long FullRoleId;
    
    [RoleName("Workspace List Role", RoleParamCode.WorkspaceList)]
    public static readonly long ReadRoleId;

    [RoleName("Workspace Add Role", RoleParamCode.WorkspaceAdd)]
    public static readonly long InsertRoleId;

    [RoleName("Workspace Update Role", RoleParamCode.WorkspaceUpdate)]
    public static readonly long UpdateRoleId;
    
    [RoleName("Workspace Delete Role", RoleParamCode.WorkspaceDelete)]
    public static readonly long DeleteRoleId;
}
