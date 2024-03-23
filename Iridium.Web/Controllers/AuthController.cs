using Iridium.Application.Services;
using Iridium.Domain.Common;
using Iridium.Domain.Models.RequestModels;
using Iridium.Domain.Models.ResponseModels;
using Iridium.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iridium.Web.Controllers;

public class AuthController : ApiBaseController
{
    public readonly AuthService AuthService;

    public AuthController(AuthService authService)
        => AuthService = authService;

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ServiceResult<UserLoginResponse>> Login([FromBody] UserLoginRequest loginRequest)
        => await AuthService.LoginAndGetUserToken(loginRequest);

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ServiceResult<bool>> Register([FromBody] UserRegisterRequest userRegisterDto)
        => await AuthService.RegisterUser(userRegisterDto);

    [AllowAnonymous]
    [HttpGet("ValidateKey")]
    public async Task<ServiceResult<bool>> ValidateKey([FromQuery] string key, [FromQuery] string guidId)
        => await AuthService.ValidateKey(key, guidId);

}
