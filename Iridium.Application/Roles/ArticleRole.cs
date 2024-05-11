using Iridium.Application.Areas;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.Roles;

public class ArticleRole : IRole
{
    [RoleName($"Article Full Role", RoleParamCode.ArticleFull)]
    public static readonly long FullRoleId;
    
    [RoleName($"Article List Role", RoleParamCode.ArticleList)]
    public static readonly long ReadRoleId;

    [RoleName($"Article Add Role", RoleParamCode.ArticleAdd)]
    public static readonly long InsertRoleId;

    [RoleName($"Update Article Role", RoleParamCode.ArticleUpdate)]
    public static readonly long UpdateRoleId;

    [RoleName($"Delete Article Role", RoleParamCode.ArticleDelete)]
    public static readonly long DeleteRoleId;
}
