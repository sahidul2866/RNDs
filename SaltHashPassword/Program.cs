using System.Security.Cryptography;

class SaltHash
{
    static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
    {
        HashAlgorithm algorithm = new SHA256Managed();

        byte[] plainTextWithSaltBytes =
          new byte[plainText.Length + salt.Length];

        for (int i = 0; i < plainText.Length; i++)
        {
            plainTextWithSaltBytes[i] = plainText[i];
        }
        for (int i = 0; i < salt.Length; i++)
        {
            plainTextWithSaltBytes[plainText.Length + i] = salt[i];
        }

        return algorithm.ComputeHash(plainTextWithSaltBytes);
    }

    public static bool CompareByteArrays(byte[] array1, byte[] array2)
    {
        if (array1.Length != array2.Length)
        {
            return false;
        }

        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
            {
                return false;
            }
        }

        return true;
    }

    private static byte[] CreateSalt(int size)
    {
        //Generate a cryptographic random number.
        var random = RandomNumberGenerator.GetBytes(size);
        //var rng = RandomNumberGenerator.Create().GetBytes(buff);
        // Return a Base64 string representation of the random number.
        return random;
    }

    public static void Main()
    {
        while(true)
            {
            Console.WriteLine("Enter the password please.");
            var pass = Console.ReadLine();
            var salt = CreateSalt(32);
            Console.WriteLine("random Salt: " + Convert.ToBase64String(salt));
            var Hash1 = GenerateSaltedHash(System.Text.Encoding.UTF8.GetBytes(pass), salt);
            Console.WriteLine("Now Re-Type the Password to check.");
            pass = Console.ReadLine();
            var Hash2 = GenerateSaltedHash(System.Text.Encoding.UTF8.GetBytes(pass), salt);
            if (CompareByteArrays(Hash1, Hash2))
            {
                Console.WriteLine("Matched");
            }
            else
            {
                Console.WriteLine("Oopsss, didn't match.");
            }
        }
    }
}
