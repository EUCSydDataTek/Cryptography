using System.Security.Cryptography;

namespace CryptographyInDotNet;
public class Hash
{
    public static byte[] GenerateSalt()
    {
        const int saltLength = 32;

        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        byte[] randomNumber = new byte[saltLength];
        randomNumberGenerator.GetBytes(randomNumber);

        return randomNumber;
    }

    private static byte[] Combine(byte[] first, byte[] second)
    {
        byte[] ret = new byte[first.Length + second.Length];

        Buffer.BlockCopy(first, 0, ret, 0, first.Length);
        Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

        return ret;
    }

    public static byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt)
    {
        using SHA256 sha256 = SHA256.Create();
        return sha256.ComputeHash(Combine(toBeHashed, salt));
    }
}
