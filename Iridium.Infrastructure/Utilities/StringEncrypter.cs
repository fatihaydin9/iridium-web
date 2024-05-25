using Iridium.Infrastructure.Constants;
using System.Security.Cryptography;
using System.Text;

namespace Iridium.Infrastructure.Utilities;

public class StringEncrypter
{
    private readonly string _entropyString;
    private readonly byte[] _entropy;
    public StringEncrypter(string symmetricKey)
    {
        _entropyString = symmetricKey;
        _entropy = Encoding.UTF8.GetBytes(_entropyString);
    }

    public string Encrypt(string value)
    {
        if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(_entropyString) && _entropy.Any())
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var encryptedBytes = ProtectedData.Protect(bytes, _entropy, DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encryptedBytes);
        }

        return value;
    }

    public string Decrypt(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            var bytes = Convert.FromBase64String(value);
            byte[] buf = ProtectedData.Unprotect(bytes, _entropy, DataProtectionScope.LocalMachine);
            return Encoding.UTF8.GetString(buf);
        }

        return value;
    }
}
