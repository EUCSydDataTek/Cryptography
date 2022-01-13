using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DecryptionSymmetricKeys
{
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
            Rfc2898DeriveBytes passwordKey = new Rfc2898DeriveBytes(password, saltValueBytes);

            // Create the algorithm and specify the key and IV
            Aes alg = Aes.Create();
            alg.Key = passwordKey.GetBytes(alg.KeySize / 8);
            alg.IV = passwordKey.GetBytes(alg.BlockSize / 8);

            // Read the encrypted file into fileData
            ICryptoTransform decryptor = alg.CreateDecryptor();
            FileStream inFile = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
            CryptoStream decryptStream = new CryptoStream(inFile, decryptor, CryptoStreamMode.Read);
            byte[] fileData = new byte[inFile.Length];
            decryptStream.Read(fileData, 0, (int)inFile.Length);

            // Write the contents of the unencrypted file
            FileStream outFile = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
            outFile.Write(fileData, 0, fileData.Length);

            // Close the file handles
            decryptStream.Close();
            inFile.Close();
            outFile.Close();
        }
    }
}
