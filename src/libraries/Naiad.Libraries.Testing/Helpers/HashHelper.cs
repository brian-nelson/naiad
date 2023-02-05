using System.Security.Cryptography;

namespace Naiad.Libraries.Testing.Helpers
{
    public static class HashHelper
    {
        public static string ComputeHash(Stream stream)
        {
            SHA256 sha = SHA256.Create();
            var result = sha.ComputeHash(stream);

            return Convert.ToBase64String(result);
        }
    }
}
