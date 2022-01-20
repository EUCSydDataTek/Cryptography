using System.Text;
using System.Security.Cryptography;

namespace AsymmetricCryptography;
class Program
{
    static void Main(string[] args)
    {
        ExportKeys();
        //StoreKeyPair();
        EncryptDecrypt();
    }

    static void ExportKeys()
    {
        // Create an instance of the RSA algorithm object
        RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider();

        // Create a new RSAParameters object with only the public key
        RSAParameters publicKey = myRSA.ExportParameters(false);

        // Create XML-files with public and public/private key
        File.WriteAllText("PublicKeyOnly.xml", myRSA.ToXmlString(false));
        File.WriteAllText("PublicPrivate.xml", myRSA.ToXmlString(true));
    }

    static void StoreKeyPair()
    {
        // Create a CspParameters object (Crypto Service Provider)
        CspParameters persistantCsp = new CspParameters();
        persistantCsp.KeyContainerName = "AsymmetricExample";

        // Create an instance of the RSA algorithm object
        RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(persistantCsp);

        // Specify that the private key should be stored in the CSP
        myRSA.PersistKeyInCsp = true;

        // Create a new RSAParameters object with the private key
        RSAParameters privateKey = myRSA.ExportParameters(true);

        // Display the private key
        foreach (byte thisByte in privateKey.D)
            Console.Write(thisByte.ToString("X2") + " ");
    }

    static void EncryptDecrypt()
    {
        string messageString = "Hello, World!";
        byte[] messageBytes = Encoding.Unicode.GetBytes(messageString);

        RSACryptoServiceProvider myRsa = new RSACryptoServiceProvider();

        // Get Public key from file and encrypt the message
        myRsa.FromXmlString(File.ReadAllText("PublicKeyOnly.xml"));
        byte[] encryptedMessage = myRsa.Encrypt(messageBytes, false);

        // Get Private key from file and decrypt the message
        myRsa.FromXmlString(File.ReadAllText("PublicPrivate.xml"));
        byte[] decryptedBytes = myRsa.Decrypt(encryptedMessage, false);

        Console.WriteLine(Encoding.Unicode.GetString(decryptedBytes));
    }
}
