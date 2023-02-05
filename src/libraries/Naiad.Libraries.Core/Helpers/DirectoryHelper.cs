using System.IO;

namespace Naiad.Libraries.Core.Helpers
{
    public static class DirectoryHelper
    {
        public static void EnsureDirectory(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }
}
