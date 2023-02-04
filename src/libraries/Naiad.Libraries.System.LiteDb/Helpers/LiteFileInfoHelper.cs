using LiteDB;
using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.LiteDb.Helpers
{
    internal static class LiteFileInfoHelper
    {
        public static NaiadFileInfo ToNaiadFileInfo(this LiteFileInfo<string> lfi)
        {
            NaiadFileInfo nfi = new NaiadFileInfo
            {
                Filename = lfi.Filename,
                Id = lfi.Id,
                MimeType = lfi.MimeType,
                Size = lfi.Length
            };

            return nfi;
        }
    }
}
