using System.Security.Cryptography;

namespace CryptographyInDotNet;
public class AesEncryption
{
    public byte[] GenerateRandomNumber(int length)
    {
        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        byte[] randomNumber = new byte[length];
        randomNumberGenerator.GetBytes(randomNumber);

        return randomNumber;
    }

    public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
    {
        using Aes aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        aes.Key = key;

        return aes.EncryptCbc(dataToEncrypt, iv);
    }

    public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
    {
        using Aes aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        aes.Key = key;

        return aes.DecryptCbc(dataToDecrypt, iv);
    }
}
