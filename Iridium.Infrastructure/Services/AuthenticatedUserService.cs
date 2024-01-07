using Microsoft.AspNetCore.Http;

namespace Iridium.Infrastructure.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = long.TryParse(httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value, out var parseValue) ? parseValue : 0;
        }
        public long UserId { get; }
    }
}