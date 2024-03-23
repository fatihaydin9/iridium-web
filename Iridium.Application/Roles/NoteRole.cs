using Iridium.Application.Areas;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;

namespace Iridium.Application.Roles;

[RoleArea("Note")]
public class NoteRole : IRole
{
    [RoleName($"Read {AreaNames.Note} Role")]
    public const string Read = $"{AreaNames.Note}.Read";

    [RoleName($"Insert {AreaNames.Note} Role")]
    public const string Insert = $"{AreaNames.Note}.Insert";

    [RoleName($"Update {AreaNames.Note} Role")]
    public const string Update = $"{AreaNames.Note}.Update";

    [RoleName($"Delete {AreaNames.Note} Role")]
    public const string Delete = $"{AreaNames.Note}.Delete";
}
