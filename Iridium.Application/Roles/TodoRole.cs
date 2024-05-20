using Iridium.Application.Areas;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.Roles;

public class TodoRole : IRole
{
    [RoleName($"Todo Full Role", RoleParamCode.TodoFull)]
    public static readonly long FullRoleId;
    
    [RoleName($"Todo List Role", RoleParamCode.TodoList)]
    public static readonly long ReadRoleId;

    [RoleName($"Todo Add Role", RoleParamCode.TodoAdd)]
    public static readonly long InsertRoleId;

    [RoleName($"Todo Update Role", RoleParamCode.TodoUpdate)]
    public static readonly long UpdateRoleId;

    [RoleName($"Todo Delete Role", RoleParamCode.TodoDelete)]
    public static readonly long DeleteRoleId;
}
