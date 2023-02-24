using System.Data;
using System.Globalization;
using System.IO;
using CsvHelper;
using Naiad.Libraries.System.Interfaces;

namespace Naiad.Libraries.System.Converters
{
    public class CsvConverter : IDataTableConverter
    {
        public DataTable Convert(Stream sourceFile)
        {
            DataTable output = null;

            using (var streamReader = new StreamReader(sourceFile))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    using (var dr = new CsvDataReader(csvReader))
                    {
                        output = new DataTable();
                        output.Load(dr);
                    }                    
                }
            }

            return output;
        }
    }
}
