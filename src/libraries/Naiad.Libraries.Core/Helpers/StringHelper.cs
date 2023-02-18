using System.Linq;

namespace Naiad.Libraries.Core.Helpers
{
    public static class StringHelper
    {
        public static bool IsOnlyAlphaNumeric(this string value)
        {
            return value.All(char.IsLetterOrDigit);
        }

    }
}
