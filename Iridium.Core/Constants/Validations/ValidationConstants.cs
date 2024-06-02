namespace Iridium.Core.Constants.Validations;

public static class ValidationConstants
{
    public static readonly string UserAlreadyExits = "Auth already exists.";
    public static readonly string MailAddressOrPasswordIsWrong = "The mail address or password is wrong";
    public static readonly string MailAddressIsInvalid = "The mail address is invalid.";

    public static readonly string PasswordShouldBeAtLeast8CharactersLong =
        "Password should be at least 8 characters long.";

    public static readonly string PasswordShouldBeMax16CharactersLong =
        "Password should be maximum 16 characters long.";

    public static readonly string PasswordAndConfirmPasswordDoNotMatch =
        "Password and confirmation password do not match.";

    public static readonly string PasswordShouldContainAtLeastOneUpperCaseLetter =
        "Password should contain at least one uppercase letter.";

    public static readonly string PasswordShouldContainAtLeastOneLowerCaseLetter =
        "Password should contain at least one lowercase letter.";

    public static readonly string PasswordShouldContainAtLeastOneDigit = "Password should contain at least one digit.";

    public static readonly string PasswordShouldContainAtLeastOneSpecialCharacter =
        "Password should contain at least one special character.";
}