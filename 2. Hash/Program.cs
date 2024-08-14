// Opret filen HashThis.txt i Debug-folderen og skriv en tekst
// Eksekver programmet: NonkeyedHash HashThis.txt
// K�r progrmmet igen og se at hashen er den samme
// �ndre lidt i filen HashThis.txt og hash igen. Nu er hashen en anden!

using System.Text;
using System.Security.Cryptography;
using System;

namespace NonkeyedHash;
class Program
{
    static void Main(string[] args)
    {
        // Hash a file with EOF, and will deviate from hash of plain string!
        using Stream fs = File.OpenRead("securefile.txt");
        byte[] hash = SHA512.Create().ComputeHash(fs);

        Console.WriteLine("Secure file.");
        Console.WriteLine("Base64:" + Convert.ToBase64String(hash));
        Console.WriteLine("Hex:" + Convert.ToHexString(hash));
        Console.WriteLine("");

        // Hash a byte array (password)
        Console.WriteLine("Text.");
        byte[] data = Encoding.UTF8.GetBytes("Kage er godt");
        byte[] hashString = SHA512.Create().ComputeHash(data);

        Console.WriteLine("Base64:" + Convert.ToBase64String(hashString));
        Console.WriteLine("Hex:" + Convert.ToHexString(hashString));
    }
}
