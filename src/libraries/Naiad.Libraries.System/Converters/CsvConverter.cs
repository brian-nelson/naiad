using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Constants.DataManagement;
using Naiad.Libraries.System.Interfaces;

namespace Naiad.Libraries.System.Converters
{
    public class CsvConverter : IDataTableConverter
    {
        public string Name => "Naiad Simple CSV Converter";

        public DataTable Convert(Stream sourceFile)
        {
            DataTable output;

            using (var streamReader = new StreamReader(sourceFile))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture);
                config.TrimOptions = TrimOptions.InsideQuotes;

                using (var csvReader = new CsvReader(streamReader, config, true))
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

        public IEnumerable<string> SupportedMimeTypes
        {
            get
            {
                var supported = new List<string> { MimeTypeConstants.CSV };
                return supported;
            }
        }
    }
}
