using System.Security.Cryptography;
using System.Text;

namespace DecryptionSymmetricKeys;
class Program
{
    static void Main(string[] args)
    {
        // Read the command-line parameters
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

        // Read the encrypted file into fileData
        using ICryptoTransform decryptor = alg.CreateDecryptor();
        using FileStream inFile = new(inFileName, FileMode.Open, FileAccess.Read);
        using CryptoStream decryptStream = new(inFile, decryptor, CryptoStreamMode.Read);
        byte[] fileData = new byte[inFile.Length];
        decryptStream.Read(fileData, 0, (int)inFile.Length);

        // Write the contents of the unencrypted file
        using FileStream outFile = new(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
        outFile.Write(fileData, 0, fileData.Length);
    }
}
