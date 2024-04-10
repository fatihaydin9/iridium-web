using Iridium.Application.Areas;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;

namespace Iridium.Application.Roles;

[RoleArea("Articles")]
public class ArticleRole : IRole
{
    [RoleName($"Read {AreaNames.Article} Role")]
    public const string Read = $"{AreaNames.Article}.Read";

    [RoleName($"Insert {AreaNames.Article} Role")]
    public const string Insert = $"{AreaNames.Article}.Insert";

    [RoleName($"Update {AreaNames.Article} Role")]
    public const string Update = $"{AreaNames.Article}.Update";

    [RoleName($"Delete {AreaNames.Article} Role")]
    public const string Delete = $"{AreaNames.Article}.Delete";
}
