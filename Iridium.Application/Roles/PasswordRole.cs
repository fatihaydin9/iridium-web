using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;

namespace Iridium.Domain.Roles;

[RoleArea("Password")]
public partial class PasswordRole : IRole
{
    [RoleName("Read Password Role")]
    public const string Read   = "Password.Read";

    [RoleName("Insert Password Role")]
    public const string Insert = "Password.Insert";

    [RoleName("Update Password Role")]
    public const string Update = "Password.Update";

    [RoleName("Delete Password Role")]
    public const string Delete = "Password.Delete";
}
