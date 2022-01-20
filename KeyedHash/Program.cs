// Opret filen HashThis.txt i Debug-folderen og skriv en tekst
// Eksekver programmet: KeyedHash P@ssw0rd HashThis.txt
// Kør progrmmet igen og se at hashen er den samme
// Ændre lidt på password og hash igen. Nu er hashen en anden!
// Ændre lidt i filen HashThis.txt og hash igen. Nu er hashen en anden!

using System.Security.Cryptography;
using System.Text;

namespace KeyedHash;
class Program
{
    static void Main(string[] args)
    {
        // Step 1: Create the secret Key, that both parties should know
        byte[] saltValueBytes = Encoding.ASCII.GetBytes("This is my salt");
        Rfc2898DeriveBytes passwordKey = new Rfc2898DeriveBytes(args[0], saltValueBytes);
        byte[] secretKey = passwordKey.GetBytes(16);

        // Step 2: Create the hash algorithm object
        HMACSHA1 myHash = new HMACSHA1(secretKey);

        // Step 3: Store the data to be hashed in a byte array
        FileStream file = new FileStream(args[1], FileMode.Open, FileAccess.Read);
        BinaryReader reader = new BinaryReader(file);

        // Step 4: Call the HashAlgorithm.ComputeHash method
        myHash.ComputeHash(reader.ReadBytes((int)file.Length));

        // Step 5: Retrieve the HashAlgorithm.Hash byte array
        Console.WriteLine(Convert.ToBase64String(myHash.Hash));
    }
}
