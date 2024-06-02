using System.Security.Cryptography;
using System.Text;

namespace Iridium.Infrastructure.Extensions;

public static class HashExtensions
{
    public static string ToSHA256Hash(this string input)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha256.ComputeHash(bytes);

            var builder = new StringBuilder();
            foreach (var b in hashBytes) builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}