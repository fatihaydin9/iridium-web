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
    public readonly AuthService _authService;

    public AuthController(AuthService authService)
        => _authService = authService;

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ServiceResult<UserLoginResponse>> Login([FromBody] UserLoginRequest loginRequest) 
        => await _authService.LoginAndGetUserToken(loginRequest);

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ServiceResult<bool>> Register([FromBody] UserRegisterRequest userRegisterDto) 
        => await _authService.RegisterUser(userRegisterDto);

}
