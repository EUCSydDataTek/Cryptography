using System.Security.Cryptography;

namespace CryptographyInDotNet;
public class DigitalSignature
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

    public byte[] SignData(byte[] hashOfDataToSign)
    {
        using RSACryptoServiceProvider rsa = new(2048);

        rsa.PersistKeyInCsp = false;
        rsa.ImportParameters(_privateKey);

        RSAPKCS1SignatureFormatter rsaFormatter = new(rsa);
        rsaFormatter.SetHashAlgorithm("SHA256");

        return rsaFormatter.CreateSignature(hashOfDataToSign);
    }

    public bool VerifySignature(byte[] hashOfDataToSign, byte[] signature)
    {
        using RSACryptoServiceProvider rsa = new(2048);

        rsa.ImportParameters(_publicKey);

        RSAPKCS1SignatureDeformatter rsaDeformatter = new(rsa);
        rsaDeformatter.SetHashAlgorithm("SHA256");

        return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);
    }
}
