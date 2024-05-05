using Iridium.Application.Areas;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.Roles;

[RoleArea("Article Area", AreaParamCode.Article)]
public class ArticleRole
{
    
    [RoleName($"Read Area Role", RoleParamCode.ArticleRead)]
    public static readonly long ReadRoleId;

    [RoleName($"Insert Article Role", RoleParamCode.ArticleInsert)]
    public static readonly long InsertRoleId;

    [RoleName($"Update Article Role", RoleParamCode.ArticleUpdate)]
    public static readonly long UpdateRoleId;

    [RoleName($"Delete Article Role", RoleParamCode.ArticleDelete)]
    public static readonly long DeleteRoleId;
    
}
