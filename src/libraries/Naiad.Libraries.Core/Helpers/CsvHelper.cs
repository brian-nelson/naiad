using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace Naiad.Libraries.Core.Helpers
{
    public static class CsvHelper
    {
        public static void WriteToCsv<T>(this IEnumerable<T> objects, string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(objects);
                }
            }
        }
    }
}
