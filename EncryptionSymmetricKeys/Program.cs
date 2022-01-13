using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SymmetricKeys
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFileName = args[0];
            string outFileName = args[1];
            string password = args[2];

            // Create the password key
            byte[] saltValueBytes = Encoding.ASCII.GetBytes("This is my sa1t");
            Rfc2898DeriveBytes passwordKey = new Rfc2898DeriveBytes(password, saltValueBytes);

            // Create the algorithm and specify the key and IV
            Aes alg = Aes.Create();
            alg.Key = passwordKey.GetBytes(alg.KeySize / 8);
            alg.IV = passwordKey.GetBytes(alg.BlockSize / 8);

            // Read the unencrypted file into fileData
            FileStream inFile = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
            byte[] fileData = new byte[inFile.Length];
            inFile.Read(fileData, 0, (int)inFile.Length);

            // Create the ICryptoTransform and CryptoStream object
            ICryptoTransform encryptor = alg.CreateEncryptor();
            FileStream outFile = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
            CryptoStream encryptStream = new CryptoStream(outFile, encryptor, CryptoStreamMode.Write);

            // Write the contents to the CryptoStream
            encryptStream.Write(fileData, 0, fileData.Length);

            // Close the file handles
            encryptStream.Close();
            inFile.Close();
            outFile.Close();
        }
    }
}
