using Iridium.Infrastructure.Constants;
using System.Security.Cryptography;
using System.Text;

namespace Iridium.Infrastructure.Utilities;

public class StringEncrypter
{
    private const string EntropyString = SymmetricKey.Value;
    private static readonly byte[] Entropy = Encoding.UTF8.GetBytes(EntropyString);

    public string Encrypt(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var encryptedBytes = ProtectedData.Protect(bytes, Entropy, DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encryptedBytes);
        }

        return value;
    }

    public string Decrypt(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            var bytes = Convert.FromBase64String(value);
            byte[] buf = ProtectedData.Unprotect(bytes, Entropy, DataProtectionScope.LocalMachine);
            return Encoding.UTF8.GetString(buf);
        }

        return value;
    }
}
