using Iridium.Infrastructure.Services.RoleSrv;
using Microsoft.Extensions.DependencyInjection;

namespace Iridium.Iridium.Infrastructure.Attributes;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

[AttributeUsage(AttributeTargets.Method)]
public class IridiumRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly IRoleService _roleService;
    public string RoleParamCode { get; }

    public IridiumRoleAttribute(string roleParamCode)
    {
        RoleParamCode = roleParamCode;
    }

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