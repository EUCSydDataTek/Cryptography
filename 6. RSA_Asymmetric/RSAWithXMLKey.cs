using System.Security.Cryptography;

namespace CryptographyInDotNet;
public class RsaWithXmlKey
{
    public void AssignNewKey(string publicKeyPath, string privateKeyPath)
    {
        using RSACryptoServiceProvider rsa = new(2048);

        rsa.PersistKeyInCsp = false;

        if (File.Exists(privateKeyPath))
        {
            File.Delete(privateKeyPath);
        }
        if (File.Exists(publicKeyPath))
        {
            File.Delete(publicKeyPath);
        }

        string publicKeyfolder = Path.GetDirectoryName(publicKeyPath);
        string privateKeyfolder = Path.GetDirectoryName(privateKeyPath);

        Directory.CreateDirectory(publicKeyfolder); // The folder is only created if it dosn't already exist (check summary)
        Directory.CreateDirectory(privateKeyfolder);// The folder is only created if it dosn't already exist (check summary)

        File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
        File.WriteAllText(privateKeyPath, rsa.ToXmlString(true));
    }

    public byte[] EncryptData(string publicKeyPath, byte[] dataToEncrypt)
    {
        using RSACryptoServiceProvider rsa = new(2048);

        rsa.PersistKeyInCsp = false;
        rsa.FromXmlString(File.ReadAllText(publicKeyPath));

        byte[] cipherText = rsa.Encrypt(dataToEncrypt, false);
        return cipherText;
    }

    public byte[] DecryptData(string privateKeyPath, byte[] dataToDecrypt)
    {
        using RSACryptoServiceProvider rsa = new(2048);

        rsa.PersistKeyInCsp = false;
        rsa.FromXmlString(File.ReadAllText(privateKeyPath));
        byte[] plainText = rsa.Decrypt(dataToDecrypt, false);
        return plainText;
    }
}
