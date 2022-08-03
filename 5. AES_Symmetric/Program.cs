using System.Text;

namespace CryptographyInDotNet;
class Program
{
    static void Main()
    {
        AesEncryption aes = new();
        byte[] key = aes.GenerateRandomNumber(32);
        byte[] iv = aes.GenerateRandomNumber(16);
        const string original = "This my secret message זרו!";

        byte[] encrypted = aes.Encrypt(Encoding.UTF8.GetBytes(original), key, iv);
        byte[] decrypted = aes.Decrypt(encrypted, key, iv);

        string decryptedMessage = Encoding.UTF8.GetString(decrypted);

        Console.WriteLine("AES Encryption Demonstration in .NET");
        Console.WriteLine("------------------------------------");
        Console.WriteLine();
        Console.WriteLine($"Original Text = {original}");
        Console.WriteLine($"Encrypted Text = {Convert.ToBase64String(encrypted)}");
        Console.WriteLine($"Decrypted Text = {decryptedMessage}");

        Console.ReadLine();
    }
}
