using System.Text.RegularExpressions;

namespace Iridium.Infrastructure.Extensions;

public static class ValidationHelper
{
    public static bool IsValidPhoneNumber(this string phone)
    {
        try
        {
            string pattern = @"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            Match match = Regex.Match(phone, pattern, RegexOptions.IgnoreCase);
            return match.Success;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidMailAddress(this string email)
    {
        try
        {
            var mail = new System.Net.Mail.MailAddress(email);
            return string.IsNullOrWhiteSpace(mail.Address) == false;
        }
        catch
        {
            return false;
        }
    }
}
