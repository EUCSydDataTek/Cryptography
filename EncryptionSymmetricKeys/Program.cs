using System.Security.Cryptography;
using System.Text;

namespace SymmetricKeys;
class Program
{
    static void Main(string[] args)
    {
        string inFileName = args[0];
        string outFileName = args[1];
        string password = args[2];

        // Create the password key
        byte[] saltValueBytes = Encoding.ASCII.GetBytes("This is my sa1t");
        using Rfc2898DeriveBytes passwordKey = new(password, saltValueBytes);

        // Create the algorithm and specify the key and IV
        using Aes alg = Aes.Create();
        alg.Key = passwordKey.GetBytes(alg.KeySize / 8);
        alg.IV = passwordKey.GetBytes(alg.BlockSize / 8);

        // Read the unencrypted file into fileData
        using FileStream inFile = new(inFileName, FileMode.Open, FileAccess.Read);
        byte[] fileData = new byte[inFile.Length];
        inFile.Read(fileData, 0, (int)inFile.Length);

        // Create the ICryptoTransform and CryptoStream object
        using ICryptoTransform encryptor = alg.CreateEncryptor();
        using FileStream outFile = new(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
        using CryptoStream encryptStream = new(outFile, encryptor, CryptoStreamMode.Write);

        // Write the contents to the CryptoStream
        encryptStream.Write(fileData, 0, fileData.Length);
    }
}
