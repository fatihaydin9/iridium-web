using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Iridium.Infrastructure.Extensions;

public static class ValidationHelper
{
    public static bool IsValidPhoneNumber(this string phone)
    {
        try
        {
            var pattern = @"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            var match = Regex.Match(phone, pattern, RegexOptions.IgnoreCase);
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
            var mail = new MailAddress(email);
            return string.IsNullOrWhiteSpace(mail.Address) == false;
        }
        catch
        {
            return false;
        }
    }
}