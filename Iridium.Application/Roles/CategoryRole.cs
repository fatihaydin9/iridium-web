using Iridium.Application.Areas;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;

namespace Iridium.Application.Roles;

[RoleArea($"{AreaNames.Workspace}")]
public class WorkspaceRole : IRole
{
    [RoleName($"Read {AreaNames.Workspace} Role")]
    public const string Read = $"{AreaNames.Workspace}.Read";

    [RoleName($"Add {AreaNames.Workspace} Role")]
    public const string Insert = $"{AreaNames.Workspace}.Insert";

    [RoleName($"Update {AreaNames.Workspace} Role")]
    public const string Update = $"{AreaNames.Workspace}.Update";

    [RoleName($"Delete {AreaNames.Workspace} Role")]
    public const string Delete = $"{AreaNames.Workspace}.Delete";
}
