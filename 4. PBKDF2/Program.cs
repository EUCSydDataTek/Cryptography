using System.Diagnostics;
using System.Text;

namespace CryptographyInDotNet;
class Program
{
    static void Main()
    {
        const string passwordToHash = "VeryComplexPassword";

        Console.WriteLine("Password Based Key Derivation Function Demonstration in .NET");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("PBKDF2 Hashes");
        Console.WriteLine();

        HashPassword(passwordToHash, 100);
        HashPassword(passwordToHash, 1000);
        HashPassword(passwordToHash, 10000);
        HashPassword(passwordToHash, 50000);
        HashPassword(passwordToHash, 100000);
        HashPassword(passwordToHash, 200000);
        HashPassword(passwordToHash, 500000);
        HashPassword(passwordToHash, 2000000);
        Console.ReadLine();
        HashPassword(passwordToHash, int.MaxValue);

        Console.ReadLine();
    }

    private static void HashPassword(string passwordToHash, int numberOfRounds)
    {
        Stopwatch sw = new();

        sw.Start();

        byte[] hashedPassword = Pbkdf2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash),
                                                 Pbkdf2.GenerateSalt(),
                                                 numberOfRounds);
        sw.Stop();

        Console.WriteLine();
        Console.WriteLine($"Password to hash : {passwordToHash}");
        Console.WriteLine($"Hashed Password : {Convert.ToBase64String(hashedPassword)}");
        Console.WriteLine($"Iterations <{numberOfRounds}> Elapsed Time : {sw.ElapsedMilliseconds}ms");
    }
}
