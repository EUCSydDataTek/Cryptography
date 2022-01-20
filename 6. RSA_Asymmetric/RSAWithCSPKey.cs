using System.Security.Cryptography;

namespace CryptographyInDotNet;
public class RsaWithCspKey
{
    const string ContainerName = "MyContainer";

    public void AssignNewKey()
    {
        CspParameters cspParams = new(1);
        cspParams.KeyContainerName = ContainerName;
        cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
        cspParams.ProviderName = "Microsoft Strong Cryptographic Provider";

        using RSACryptoServiceProvider rsa = new(cspParams) { PersistKeyInCsp = true };
    }

    public void DeleteKeyInCsp()
    {
        CspParameters cspParams = new() { KeyContainerName = ContainerName };
        using RSACryptoServiceProvider rsa = new(cspParams) { PersistKeyInCsp = false };

        rsa.Clear();
    }

    public byte[] EncryptData(byte[] dataToEncrypt)
    {
        CspParameters cspParams = new() { KeyContainerName = ContainerName };

        using RSACryptoServiceProvider rsa = new(2048, cspParams);

        byte[] cipherbytes = rsa.Encrypt(dataToEncrypt, false);
        return cipherbytes;
    }

    public byte[] DecryptData(byte[] dataToDecrypt)
    {
        CspParameters cspParams = new() { KeyContainerName = ContainerName };
        using RSACryptoServiceProvider rsa = new(2048, cspParams);

        byte[] plainText = rsa.Decrypt(dataToDecrypt, false);
        return plainText;
    }
}
