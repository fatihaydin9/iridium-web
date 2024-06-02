using System.Security.Cryptography;
using System.Text;

namespace Iridium.Infrastructure.Extensions;

public static class HashExtensions
{
    public static string ToSHA256Hash(this string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                builder.Append(b.ToString("x2")); 
            }
            return builder.ToString();
        }
    }

}
