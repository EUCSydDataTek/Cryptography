using System.Security.Cryptography;

namespace CryptographyInDotNet;
public class RsaWithRsaParameterKey
{
    private RSAParameters _publicKey;
    private RSAParameters _privateKey;

    public void AssignNewKey()
    {
        using RSACryptoServiceProvider rsa = new(2048);

        rsa.PersistKeyInCsp = false;
        _publicKey = rsa.ExportParameters(false);
        _privateKey = rsa.ExportParameters(true);
    }

    public byte[] EncryptData(byte[] dataToEncrypt)
    {
        using RSACryptoServiceProvider rsa = new(2048);
        
        rsa.PersistKeyInCsp = false;
        rsa.ImportParameters(_publicKey);

        byte[] cipherbytes = rsa.Encrypt(dataToEncrypt, false);
        return cipherbytes;
    }

    public byte[] DecryptData(byte[] dataToEncrypt)
    {
        using RSACryptoServiceProvider rsa = new(2048);
        
        rsa.PersistKeyInCsp = false;
        rsa.ImportParameters(_privateKey);
        
        byte[] plain = rsa.Decrypt(dataToEncrypt, false);
        return plain;
    }
}
