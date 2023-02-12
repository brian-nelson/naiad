using System;
using System.Security.Cryptography;
using System.Text;

namespace Naiad.Libraries.Core.Helpers;

public static class PasswordHelper
{
    private const int SALT_BYTES = 20;
    private const int HASH_BYTES = 20;
    private const int ITERATIONS = 10000;

    public const int DefaultAdminPasswordLength = 20;

    public static readonly string[] Characters =
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
        "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
        "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
        "e", "f", "g", "h", "i", "j", "k", "l", "m", "n",
        "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
        "y", "z", "1", "2", "3", "4", "5", "6", "7", "8",
        "9", "0", "!", "@", "#", "$", "%", "^", "&", "*"
    };

    public static string Hash(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);

        if (saltBytes.Length != SALT_BYTES)
        {
            throw new ArgumentOutOfRangeException(nameof(salt));
        }

        byte[] hashBytes;

        using (var hasher = new Rfc2898DeriveBytes(
                   password: password,
                   salt: saltBytes,
                   iterations: ITERATIONS))
        {
            hashBytes = hasher.GetBytes(HASH_BYTES);
        }

        return Convert.ToBase64String(hashBytes);
    }

    public static string GenerateSalt()
    {
        var salt = GetRandomArray(SALT_BYTES);
        return Convert.ToBase64String(salt);
    }

    public static string GeneratePassword(int _length)
    {
        var passwordChars = Characters.Length;

        StringBuilder sb = new StringBuilder();

        int[] array = GetRandomArrayOfInts(_length);

        for (int i = 0; i < _length; i++)
        {
            int b = array[i] % passwordChars;

            sb.Append(Characters[b]);
        }

        return sb.ToString();
    }

    private static byte[] GetRandomArray(int _length)
    {
        var array = RandomNumberGenerator.GetBytes(_length);
        return array;
    }

    private static int[] GetRandomArrayOfInts(int _length)
    {
        var nums = new int[_length];

        for (int i = 0; i < _length; i++)
        {
            nums[i] = RandomNumberGenerator.GetInt32(Int32.MaxValue);
        }

        return nums;
    }
}
