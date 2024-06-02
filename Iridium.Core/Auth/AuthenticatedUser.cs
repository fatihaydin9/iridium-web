using Microsoft.AspNetCore.Http;

namespace Iridium.Core.Auth;

public class AuthenticatedUser : IAuthenticatedUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        UserId = long.TryParse(httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value, out var parseValue) ? parseValue : 0;
    }
    public long UserId { get; }
}

public class MockUserService : IAuthenticatedUser
{
    public long UserId { get; private set; }

    public MockUserService(long userId = 1)  
    {
        UserId = userId;
    }
}
