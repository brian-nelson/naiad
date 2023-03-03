using System.Security.Cryptography;
using System.Text;

namespace Naiad.Libraries.Testing.Helpers;

public static class RandomHelper
{
    private static readonly string[] Characters =
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
        "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
        "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
        "e", "f", "g", "h", "i", "j", "k", "l", "m", "n",
        "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
        "y", "z", "1", "2", "3", "4", "5", "6", "7", "8",
        "9", "0", "!", "-"
    };



    public static string GetRandomAlphaString(int length)
    {
        StringBuilder sb = new StringBuilder();

        Byte[] array = GetRandomArray(length);

        for (int i = 0; i < length; i++)
        {
            int b = array[i] % 52;

            sb.Append(Characters[b]);
        }

        return sb.ToString();
    }

    public static string GetRandomFilename(int parts)
    {
        var sb = new StringBuilder();

        sb.Append("/");

        for (int i = 0; i < parts - 1; i++)
        {
            sb.Append(GetRandomAlphaString(10));
            sb.Append("/");
        }

        sb.Append(GetRandomAlphaNumericString(10));
        sb.Append(".");
        sb.Append(GetRandomAlphaNumericString(3));

        return sb.ToString();
    }

    public static string GetRandomAlphaNumericString(int _length)
    {
        StringBuilder sb = new StringBuilder();

        Byte[] array = GetRandomArray(_length);

        for (int i = 0; i < _length; i++)
        {
            int b = array[i] % 62;

            sb.Append(Characters[b]);
        }

        return sb.ToString();
    }

    public static DateTimeOffset GetRandomDateTimeOffset(int days)
    {
        var now = DateTimeOffset.UtcNow;

        var delta = RandomHelper.GetRandomInt(-days, days);
        var output = now.AddDays(delta);

        return output;
    }


    private static byte[] GetRandomArray(int _length)
    {
        var array = RandomNumberGenerator.GetBytes(_length);
        return array;
    }

    public static int GetRandomInt(int min, int max)
    {
        return RandomNumberGenerator.GetInt32(min, max);
    }

    public static double GetRandomDouble(int min, int max)
    {
        var random = new Random();

        return random.NextDouble() * (max - min) + min;
    }
}
