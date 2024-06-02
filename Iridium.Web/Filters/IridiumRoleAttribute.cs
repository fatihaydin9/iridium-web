using System.Security.Claims;
using Iridium.Application.Services.RoleSrv;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Iridium.Web.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class IridiumRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly IRoleService _roleService;

    public IridiumRoleAttribute(string roleParamCode)
    {
        RoleParamCode = roleParamCode;
    }

    public string RoleParamCode { get; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var roleService = context.HttpContext.RequestServices.GetService(typeof(IRoleService)) as IRoleService;

        if (roleService == null)
            throw new InvalidOperationException("RoleService not registered.");

        var user = context.HttpContext.User;
        if (user == null || !user.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException();

        var userRoleParamCodes = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

        if (!userRoleParamCodes.Contains(RoleParamCode))
            throw new UnauthorizedAccessException();
    }
}