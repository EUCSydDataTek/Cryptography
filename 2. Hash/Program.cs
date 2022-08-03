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
        // Hash a file with EOF, and will deviate from hash of plain string!
        using Stream fs = File.OpenRead(args[0]);
        byte[] hash = MD5.Create().ComputeHash(fs);
        Console.WriteLine(Convert.ToBase64String(hash));

        // Hash a byte array (password)
        byte[] data = Encoding.UTF8.GetBytes("Kage er godt");
        byte[] hashString = MD5.Create().ComputeHash(data);
        Console.WriteLine(Convert.ToBase64String(hashString));
        Console.WriteLine(Convert.ToHexString(hashString));
    }
}
