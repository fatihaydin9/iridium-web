using Iridium.Application.Areas;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.Roles;

[RoleArea("Workspace Area", AreaParamCode.Workspace)]
public class WorkspaceRole : IRole
{
    
    [RoleName("Read Workspace Role", RoleParamCode.WorkspaceRead)]
    public static readonly long ReadRoleId;

    [RoleName("Insert Workspace Role", RoleParamCode.WorkspaceInsert)]
    public static readonly long InsertRoleId;

    [RoleName("Update Workspace Role", RoleParamCode.WorkspaceUpdate)]
    public static readonly long UpdateRoleId;
    
    [RoleName("Delete Workspace Role", RoleParamCode.WorkspaceDelete)]
    public static readonly long DeleteRoleId;

}
