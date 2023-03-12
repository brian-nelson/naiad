using System.Linq;

namespace Naiad.Libraries.Core.Helpers
{
    public static class StringHelper
    {
        public static bool IsOnlyAlphaNumeric(this string value)
        {
            return value.All(char.IsLetterOrDigit);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
