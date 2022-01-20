// Opret filen HashThis.txt i Debug-folderen og skriv en tekst
// Eksekver programmet: NonkeyedHash HashThis.txt
// K�r progrmmet igen og se at hashen er den samme
// �ndre lidt i filen HashThis.txt og hash igen. Nu er hashen en anden!

using System.Text;
using System.Security.Cryptography;

namespace NonkeyedHash;
class Program
{
    static void Main(string[] args)
    {
        // Hash a file
        using (Stream fs = File.OpenRead(args[0]))
        {
            byte[] hash = MD5.Create().ComputeHash(fs);
            Console.WriteLine(Convert.ToBase64String(hash));

            // Hash a byte array (password)
            byte[] data = Encoding.UTF8.GetBytes("password");
            hash = SHA256.Create().ComputeHash(data);
            Console.WriteLine(Convert.ToBase64String(hash));
        }
    }
}
