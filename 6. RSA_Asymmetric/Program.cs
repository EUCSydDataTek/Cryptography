using System.Text;

namespace CryptographyInDotNet;
class Program
{
    static void Main()
    {
        RsaWithRsaParameterKey();
        //RsaWithXml();
        //RsaWithCsp();

        Console.ReadLine();
    }

    private static void RsaWithRsaParameterKey()
    {
        RSAWithRSAParameterKey rsaParams = new();
        const string original = "Text to encrypt";

        rsaParams.AssignNewKey();

        byte[] encryptedRsaParams = rsaParams.EncryptData(Encoding.UTF8.GetBytes(original));
        byte[] decryptedRsaParams = rsaParams.DecryptData(encryptedRsaParams);


        Console.WriteLine("RSA Encryption Demonstration in .NET");
        Console.WriteLine("------------------------------------");
        Console.WriteLine();
        Console.WriteLine("In Memory Key");
        Console.WriteLine();
        Console.WriteLine($"   Original Text = {original}");
        Console.WriteLine();
        Console.WriteLine($"   Encrypted Text = {Convert.ToBase64String(encryptedRsaParams)}");
        Console.WriteLine();
        Console.WriteLine($"   Decrypted Text = {Encoding.UTF8.GetString(decryptedRsaParams)}");
        Console.WriteLine();
        Console.WriteLine();
    }

    private static void RsaWithXml()
    {
        RsaWithXmlKey rsa = new();

        const string original = "Text to encrypt";
        const string publicKeyPath = "c:\\temp\\publickey.xml";
        const string privateKeyPath = "c:\\temp\\privatekey.xml";

        rsa.AssignNewKey(publicKeyPath, privateKeyPath);
        byte[] encrypted = rsa.EncryptData(publicKeyPath, Encoding.UTF8.GetBytes(original));
        byte[] decrypted = rsa.DecryptData(privateKeyPath, encrypted);

        Console.WriteLine("Xml Based Key");
        Console.WriteLine();
        Console.WriteLine($"   Original Text = {original}");
        Console.WriteLine();
        Console.WriteLine($"   Encrypted Text = {Convert.ToBase64String(encrypted)}");
        Console.WriteLine();
        Console.WriteLine($"   Decrypted Text = {Encoding.UTF8.GetString(decrypted)}");
        Console.WriteLine();
    }

    private static void RsaWithCsp()
    {
        RsaWithCspKey rsaCsp = new();
        const string original = "Text to encrypt";

        rsaCsp.AssignNewKey();

        byte[] encryptedCsp = rsaCsp.EncryptData(Encoding.UTF8.GetBytes(original));
        byte[] decryptedCsp = rsaCsp.DecryptData(encryptedCsp);

        rsaCsp.DeleteKeyInCsp();

        Console.WriteLine();
        Console.WriteLine("CSP Based Key");
        Console.WriteLine();
        Console.WriteLine($"   Original Text = {original}");
        Console.WriteLine();
        Console.WriteLine($"   Encrypted Text = {Convert.ToBase64String(encryptedCsp)}");
        Console.WriteLine();
        Console.WriteLine($"   Decrypted Text = {Encoding.UTF8.GetString(decryptedCsp)}");
    }
}
