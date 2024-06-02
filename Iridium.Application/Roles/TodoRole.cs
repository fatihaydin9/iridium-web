using Iridium.Domain.Common;
using Iridium.Core.Attributes;
using Iridium.Core.Constants;

namespace Iridium.Application.Roles;

public class TodoRole : IRole
{
    [RoleName($"Todos Full Role", RoleParamCode.TodoFull)]
    public static readonly long FullRoleId;
    
    [RoleName($"Todos List Role", RoleParamCode.TodoList)]
    public static readonly long ReadRoleId;

    [RoleName($"Todos Add Role", RoleParamCode.TodoAdd)]
    public static readonly long InsertRoleId;

    [RoleName($"Todos Update Role", RoleParamCode.TodoUpdate)]
    public static readonly long UpdateRoleId;

    [RoleName($"Todos Delete Role", RoleParamCode.TodoDelete)]
    public static readonly long DeleteRoleId;
}
