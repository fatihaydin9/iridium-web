using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

[AttributeUsage(AttributeTargets.Method)]
public class IridiumRoleAttribute : Attribute, IAuthorizationFilter
{
    public string RoleParamCode { get; }

    public IridiumRoleAttribute(string roleParamCode)
    {
        RoleParamCode = roleParamCode;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user == null || user.Identity == null || !user.Identity.IsAuthenticated || 
            !user.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == RoleParamCode))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}