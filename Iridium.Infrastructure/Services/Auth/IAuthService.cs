using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Models.RequestModels;
using Iridium.Domain.Models.ResponseModels;

namespace Iridium.Application.Services;

public interface IAuthService
{
    Task<ServiceResult<bool>> RegisterUser(UserRegisterRequest registerRequest);

    Task<ServiceResult<User>> GetAuthenticatedUser(UserLoginRequest loginRequest);

    Task<ServiceResult<UserLoginResponse>> LoginAndGetUserToken(UserLoginRequest loginRequest);
}