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

namespace Iridium.Application.Services;

public class AuthService
{
    private readonly ApplicationDbContext dbContext;
    private readonly AppSettings appSettings;


    public AuthService(ApplicationDbContext dbContext, IOptions<AppSettings> appSettings)
    {
        this.dbContext = dbContext;
        this.appSettings = appSettings.Value;
    }

    public async Task<ServiceResult<bool>> RegisterUser(UserRegisterRequest registerRequest)
    {
        var result = new ServiceResult<bool>();

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

        if (!registerRequest.Password.Any(ch => !char.IsLetterOrDigit(ch)))
            return new ServiceResult<bool>("Password should contain at least one special character.");

        if (!registerRequest.PhoneNumber.IsValidPhoneNumber())
            return new ServiceResult<bool>("The phone number is invalid.");

        if (!registerRequest.MailAddress.IsValidMailAddress())
            return new ServiceResult<bool>("The email address is invalid.");

        var hashedPassword = registerRequest.Password.ToSHA256Hash();
        var validationKey = Guid.NewGuid().ToString(); // for mail validation

        var registeredUser = await dbContext.User.Where(w => w.Deleted != true && w.MailAddress == registerRequest.MailAddress)
                                                 .FirstOrDefaultAsync();

        if (registeredUser != null)
        {
            if (registeredUser.UserState == (short)UserState.Registered)
                return new ServiceResult<bool>("User already registered : waiting for e-mail validation.");
            else
                return new ServiceResult<bool>("User already exists.");
        }

        var user = new User
        {
            MailAddress = registerRequest.MailAddress,
            Password = hashedPassword,
            PhoneNumber = registerRequest.PhoneNumber,
            ValidationKey = validationKey,
            ValidationExpire = DateTime.UtcNow,
            UserState = (short)UserState.Registered,
            IsPremium = false,
            CreatedBy = 1,
            CreatedDate = DateTime.UtcNow,
        };

        await dbContext.User.AddAsync(user);

        await dbContext.SaveChangesAsync();

        return new ServiceResult<bool>(true);
    }

    public async Task<ServiceResult<User>> GetAuthenticatedUser(UserLoginRequest loginRequest)
    {
        var result = new ServiceResult<User>();
        var hashedPassword = loginRequest.Password.ToSHA256Hash();

        var user = await dbContext.User.Where(w => w.MailAddress == loginRequest.MailAddress)
                                       .Where(w => w.Password == hashedPassword)
                                       .FirstOrDefaultAsync();

        if (user == null)
            return new ServiceResult<User>("The mail address or password is wrong.");

        return new ServiceResult<User>(user);
    }

    public async Task<ServiceResult<UserLoginResponse>> LoginAndGetUserToken(UserLoginRequest loginRequest)
    {
        var hashedPassword = loginRequest.Password.ToSHA256Hash();

        var user = await dbContext.User.Where(w => w.MailAddress == loginRequest.MailAddress)
                                       .Where(w => w.Password == hashedPassword)
                                       .FirstOrDefaultAsync();

        if (user == null)
            return new ServiceResult<UserLoginResponse>("The mail address or password is wrong.");

        if (user.UserState == (short)UserState.Registered)
        {
            if (user.ValidationExpire < DateTime.UtcNow)
            {
                return new ServiceResult<UserLoginResponse>("To continue, verify your e-mail address by clicking on the link sent to your e-mail.");
            }
            else
            {
                user.ValidationKey = Guid.NewGuid().ToString();
                user.ValidationExpire = DateTime.UtcNow;

                dbContext.User.Update(user);

                await dbContext.SaveChangesAsync();

                SendValidationMailToUser(user.GuidId.ToString(), user.ValidationKey, user.MailAddress);

                return new ServiceResult<UserLoginResponse>("To continue, verify your e-mail address by clicking on the new link sent to your e-mail.");
            }
        }

        return LoginUserAndGenerateJWTToken(user);
    }

    public async Task<ServiceResult<bool>> ValidateKey(string key, string guidId)
    {
        var user = await dbContext.User.Where(w => w.Deleted != true &&
                                                   w.UserState == (short)UserState.Completed &&
                                                   w.ValidationKey == key &&
                                                   w.GuidId.ToString() == guidId)
                                       .FirstOrDefaultAsync();

        if (user == null)
            return new ServiceResult<bool>("Validation link expired or not found.");

        if (user.ValidationExpire < DateTime.UtcNow)
        {
            user.UserState = (short)UserState.Completed;
            dbContext.User.Update(user);
            await dbContext.SaveChangesAsync();

            return new ServiceResult<bool>(true);
        }
        else
        {
            SendValidationMailToUser(user.GuidId.ToString(), user.ValidationKey, user.MailAddress);

            return new ServiceResult<bool>("To continue, verify your e-mail address by clicking on the new link sent to your e-mail.");
        }
    }

    #region Private Methods 

    private ServiceResult<UserLoginResponse> LoginUserAndGenerateJWTToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SymmetricKey.Value);

        var userRoles = dbContext.UserRole.Include(ur => ur.Role)
                                          .Where(ur => ur.UserId == user.Id)
                                          .Select(ur => ur.Role.ParamCode)
                                          .ToList();

        var claims = new List<Claim>{
            new Claim(ClaimTypes.NameIdentifier, user.MailAddress),
        };

        foreach (var role in userRoles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var tokenExpireDate = DateTime.UtcNow.AddDays(ConfigurationConstants.TOKEN_EXPIRE_AS_DAY);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = tokenExpireDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
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
        }, out SecurityToken validatedToken);

        return validatedToken != null;
    }
    private void SendValidationMailToUser(string userGuidId, string validationKey, string mailAddress)
    {
        var baseUrl = appSettings.Base.Url;
        var mailClientSettings = appSettings.MailClientSettings;

        var subject = MailConstants.RegistrationMailSubject;

        var validationLink = $"{baseUrl}/Auth/ValidateKey?key={validationKey}&guidId={userGuidId}";
        var message = $@"{MailConstants.RegistrationMailMessage} : {validationLink} ";

        MailClient mailClient = new MailClient(mailClientSettings.Mail, mailClientSettings.Username, mailClientSettings.Password, mailClientSettings.Address);

        mailClient.SendEmail(mailAddress, subject, message);
    }

    #endregion

}
