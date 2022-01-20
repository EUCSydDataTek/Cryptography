namespace CryptographyInDotNet;
public class HybridEncryption
{
    private readonly AesEncryption _aes = new();

    public EncryptedPacket EncryptData(byte[] original, RsaWithRsaParameterKey rsaParams)
    {
        // Generate our session key.
        byte[] sessionKey = _aes.GenerateRandomNumber(32);

        // Create the encrypted packet and generate the IV.
        EncryptedPacket encryptedPacket = new() { Iv = _aes.GenerateRandomNumber(16) };

        // Encrypt our data with AES.
        encryptedPacket.EncryptedData = _aes.Encrypt(original, sessionKey, encryptedPacket.Iv);

        // Encrypt the session key with RSA
        encryptedPacket.EncryptedSessionKey = rsaParams.EncryptData(sessionKey);

        return encryptedPacket;
    }

    public byte[] DecryptData(EncryptedPacket encryptedPacket, RsaWithRsaParameterKey rsaParams)
    {
        // Decrypt AES Key with RSA.
        byte[] decryptedSessionKey = rsaParams.DecryptData(encryptedPacket.EncryptedSessionKey);

        // Decrypt our data with  AES using the decrypted session key.
        byte[] decryptedData = _aes.Decrypt(encryptedPacket.EncryptedData,
                                         decryptedSessionKey, encryptedPacket.Iv);

        return decryptedData;
    }
}
