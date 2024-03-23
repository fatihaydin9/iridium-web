using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;

namespace Iridium.Application.Roles;

[RoleArea("Note")]
public abstract class NoteRole : IRole
{
    [RoleName("Read Note Role")]
    public const string Read   = "Note.Read";

    [RoleName("Insert Note Role")]
    public const string Insert = "Note.Insert";

    [RoleName("Update Note Role")]
    public const string Update = "Note.Update";

    [RoleName("Delete Note Role")]
    public const string Delete = "Note.Delete";
}
