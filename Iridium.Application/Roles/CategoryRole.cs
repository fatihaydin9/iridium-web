using Iridium.Application.Areas;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;

namespace Iridium.Application.Roles;

[RoleArea($"{AreaNames.Category}")]
public class CategoryRole : IRole
{
    [RoleName($"Read {AreaNames.Category} Role")]
    public const string Read = $"{AreaNames.Category}.Read";

    [RoleName($"Add {AreaNames.Category} Role")]
    public const string Insert = $"{AreaNames.Category}.Insert";

    [RoleName($"$Update {AreaNames.Category} Role")]
    public const string Update = $"{AreaNames.Category}.Update";

    [RoleName($"Delete {AreaNames.Category} Role")]
    public const string Delete = $"{AreaNames.Category}.Delete";
}
