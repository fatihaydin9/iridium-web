using Iridium.Application.Services;
using Microsoft.AspNetCore.Http;

namespace Iridium.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        UserId = long.TryParse(httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value, out var parseValue) ? parseValue : 0;
    }
    public long UserId { get; }
}

public class MockUserService : IUserService
{
    public long UserId { get; private set; }

    public MockUserService(long userId = 1)  
    {
        UserId = userId;
    }
}
