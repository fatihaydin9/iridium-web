using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Domain.Enums;
using Iridium.Domain.Models.RequestModels;
using Iridium.Domain.Models.ResponseModels;
using Iridium.Infrastructure.Constants;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Extensions;
using Iridium.Infrastructure.Models;
using Iridium.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Iridium.Application.Areas;
using Iridium.Application.Roles;

namespace Iridium.Application.Services;

public class AuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly AppSettings _appSettings;

    public AuthService(ApplicationDbContext dbContext, IOptions<AppSettings> appSettings)
    {
        _dbContext = dbContext;
        _appSettings = appSettings.Value;
    }

    public async Task<ServiceResult<bool>> RegisterUser(UserRegisterRequest registerRequest)
    {
        // TODO: Phone Validation
        //if (!registerRequest.PhoneNumber.IsValidPhoneNumber())
        //    return new ServiceResult<bool>("The phone number is invalid.");
        // TODO: Mail Client and Commercial Mail System

        var isUserExists = await _dbContext.User.AnyAsync(
            w => w.Deleted != true && w.MailAddress == registerRequest.MailAddress);

        if (isUserExists)
            return new ServiceResult<bool>("User already exists.");

        if (!registerRequest.MailAddress.IsValidMailAddress())
            return new ServiceResult<bool>("The email address is invalid.");

        if (registerRequest.Password.Length < 8)
            return new ServiceResult<bool>("Password should be at least 8 characters long.");

        if (registerRequest.Password != registerRequest.PasswordConfirm)
            return new ServiceResult<bool>("Password and confirmation password do not match.");

        if (!registerRequest.Password.Any(char.IsUpper))
            return new ServiceResult<bool>("Password should contain at least one uppercase letter.");

        if (!registerRequest.Password.Any(char.IsLower))
            return new ServiceResult<bool>("Password should contain at least one lowercase letter.");

        if (!registerRequest.Password.Any(char.IsDigit))
            return new ServiceResult<bool>("Password should contain at least one digit.");

        if (registerRequest.Password.All(char.IsLetterOrDigit))
            return new ServiceResult<bool>("Password should contain at least one special character.");

        var hashedPassword = registerRequest.Password.ToSHA256Hash();
        var validationKey = Guid.NewGuid().ToString(); // for mail validation

        var registeredUser = await _dbContext.User
            .Where(w => w.Deleted != true && w.MailAddress == registerRequest.MailAddress)
            .FirstOrDefaultAsync();

        if (registeredUser != null)
        {
            return registeredUser.UserState == (short)UserState.Registered
                ? new ServiceResult<bool>("User already registered : waiting for e-mail validation.")
                : new ServiceResult<bool>("User already exists.");
        }

        // for adding user
        var user = new User
        {
            MailAddress = registerRequest.MailAddress,
            Password = hashedPassword,
            PhoneNumber = registerRequest.PhoneNumber,
            ValidationKey = validationKey,
            ValidationExpire = DateTime.UtcNow,
            UserState = (short)UserState.Completed,
            IsPremium = false,
            CreatedBy = 1,
            CreatedDate = DateTime.UtcNow
        };

        await _dbContext.User.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        // for adding standard user roles
        var roleIds = new List<long>() { ArticleRole.FullRoleId, WorkspaceRole.FullRoleId };

        var userId = user.Id;
        var userRoles = new List<UserRole>();

        foreach (var roleId in roleIds)
            userRoles.Add(new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            });

        await _dbContext.UserRole.AddRangeAsync(userRoles);
        await _dbContext.SaveChangesAsync();

        return new ServiceResult<bool>(true);
    }

    public async Task<ServiceResult<User>> GetAuthenticatedUser(UserLoginRequest loginRequest)
    {
        var hashedPassword = loginRequest.Password.ToSHA256Hash();

        var user = await _dbContext.User.Where(w => w.MailAddress == loginRequest.MailAddress)
            .Where(w => w.Password == hashedPassword)
            .FirstOrDefaultAsync();

        return user == null
            ? new ServiceResult<User>("The mail address or password is wrong.")
            : new ServiceResult<User>(user);
    }

    public async Task<ServiceResult<UserLoginResponse>> LoginAndGetUserToken(UserLoginRequest loginRequest)
    {
        var hashedPassword = loginRequest.Password.ToSHA256Hash();

        var user = await _dbContext.User.Where(w => w.MailAddress == loginRequest.MailAddress)
            .Where(w => w.Password == hashedPassword)
            .FirstOrDefaultAsync();

        if (user == null)
            return new ServiceResult<UserLoginResponse>("The mail address or password is wrong.");

        if (user.UserState != (short)UserState.Registered)
            return LoginUserAndGenerateJwtToken(user);

        if (user.ValidationExpire < DateTime.UtcNow)
            return new ServiceResult<UserLoginResponse>("To continue, verify your e-mail address by clicking on the link sent to your e-mail.");

        user.ValidationKey = Guid.NewGuid().ToString();
        user.ValidationExpire = DateTime.UtcNow;

        _dbContext.User.Update(user);

        await _dbContext.SaveChangesAsync();

        SendValidationMailToUser(user.GuidId.ToString(), user.ValidationKey, user.MailAddress);

        return new ServiceResult<UserLoginResponse>("To continue, verify your e-mail address by clicking on the new link sent to your e-mail.");
    }

    public async Task<ServiceResult<bool>> ValidateKey(string key, string guidId)
    {
        var user = await _dbContext.User.Where(w => w.Deleted != true &&
                                                    w.UserState == (short)UserState.Completed &&
                                                    w.ValidationKey == key &&
                                                    w.GuidId.ToString() == guidId)
            .FirstOrDefaultAsync();

        if (user == null)
            return new ServiceResult<bool>("Validation link expired or not found.");

        if (user.ValidationExpire < DateTime.UtcNow)
        {
            user.UserState = (short)UserState.Completed;
            _dbContext.User.Update(user);
            await _dbContext.SaveChangesAsync();

            return new ServiceResult<bool>(true);
        }

        user.ValidationKey = Guid.NewGuid().ToString();
        _dbContext.User.Update(user);
        await _dbContext.SaveChangesAsync();

        SendValidationMailToUser(user.GuidId.ToString(), user.ValidationKey, user.MailAddress);

        return new ServiceResult<bool>("To continue, verify your e-mail address by clicking on the new link sent to your e-mail.");
    }

    #region Private Methods

    private ServiceResult<UserLoginResponse> LoginUserAndGenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SymmetricKey.Value);

        var userRoles = _dbContext.UserRole.Include(ur => ur.Role)
            .Where(ur => ur.UserId == user.Id)
            .Select(ur => ur.Role.ParamCode)
            .ToList();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.MailAddress),
        };

        foreach (var role in userRoles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var tokenExpireDate = DateTime.UtcNow.AddDays(ConfigurationConstants.TokenExpireAsDay);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = tokenExpireDate,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        var loginResponse = new UserLoginResponse()
        {
            AccessToken = accessToken,
            ExpiresIn = tokenExpireDate
        };

        return new ServiceResult<UserLoginResponse>(loginResponse);
    }

    private bool ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SymmetricKey.Value);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        return validatedToken != null;
    }

    private void SendValidationMailToUser(string userGuidId, string validationKey, string mailAddress)
    {
        var baseUrl = _appSettings.Base.Url;
        var mailClientSettings = _appSettings.MailClientSettings;

        var subject = MailConfigurations.RegistrationMailSubject;

        var validationLink = $"{baseUrl}/Auth/ValidateKey?key={validationKey}&guidId={userGuidId}";
        var message = $@"{MailConfigurations.RegistrationMailMessage} : {validationLink} ";

        var mailClient = new MailClient(mailClientSettings.Mail, mailClientSettings.Username,
            mailClientSettings.Password, mailClientSettings.Address);

        mailClient.SendEmail(mailAddress, subject, message);
    }

    #endregion
}