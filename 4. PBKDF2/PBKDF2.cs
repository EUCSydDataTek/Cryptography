using System.Security.Cryptography;

namespace CryptographyInDotNet;
public class Pbkdf2
{
    public static byte[] GenerateSalt()
    {
        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        byte[] randomNumber = new byte[32];
        randomNumberGenerator.GetBytes(randomNumber);

        return randomNumber;
    }

    public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
    {
        using Rfc2898DeriveBytes rfc2898 = new(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256);
        return rfc2898.GetBytes(32);
    }
}
