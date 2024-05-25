using Iridium.Infrastructure.Constants;
using System.Security.Cryptography;
using System.Text;

namespace Iridium.Infrastructure.Utilities;

public class AESEncrypter
{
    public static string EncryptString(string plainText, string key)
    {
        var iv = new byte[16];
        byte[] array;

        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    public static string DecryptString(string cipherText, string key)
    {
        var iv = new byte[16];
        var buffer = Convert.FromBase64String(cipherText);

        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream(buffer))
            {
                using (var cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (var streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }



}
