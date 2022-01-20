using System.Text;

namespace CryptographyInDotNet;
class Program
{
    static void Main()
    {
        const string original = "Very secret and important information that can not fall into the wrong hands.";

        RsaWithRsaParameterKey rsaParams = new();
        rsaParams.AssignNewKey();

        HybridEncryption hybrid = new();

        EncryptedPacket encryptedBlock = hybrid.EncryptData(Encoding.UTF8.GetBytes(original), rsaParams);
        byte[] decrypted = hybrid.DecryptData(encryptedBlock, rsaParams);

        Console.WriteLine("Hybrid Encryption Demonstration in .NET");
        Console.WriteLine("---------------------------------------");
        Console.WriteLine();
        Console.WriteLine($"Original Message = {original}");
        Console.WriteLine();
        Console.WriteLine($"Message After Decryption = {Encoding.UTF8.GetString(decrypted)}");
        Console.ReadLine();
    }
}
