using System.Security.Cryptography;
using System.Text;

namespace CryptographyInDotNet;
class Program
{
    static void Main()
    {
        byte[] document = Encoding.UTF8.GetBytes("Document to Sign");

        using SHA256 sha256 = SHA256.Create();
        byte[] hashedDocument = sha256.ComputeHash(document);

        DigitalSignature digitalSignature = new();
        digitalSignature.AssignNewKey();

        byte[] signature = digitalSignature.SignData(hashedDocument);
        bool verified = digitalSignature.VerifySignature(hashedDocument, signature);

        Console.WriteLine("Digital Signature Demonstration in .NET");
        Console.WriteLine("---------------------------------------");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"   Original Text = {Encoding.UTF8.GetString(document)}");

        Console.WriteLine();
        Console.WriteLine($"   Digital Signature = {Convert.ToBase64String(signature)}");

        Console.WriteLine();

        Console.WriteLine(verified ?
             "The digital signature has been correctly verified."
            : "The digital signature has NOT been correctly verified.");

        Console.ReadLine();
    }
}
