﻿using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Naiad.Libraries.System.Constants.DataManagement;
using Naiad.Libraries.System.Interfaces;

namespace Naiad.Libraries.System.Converters
{
    public class CsvConverter : IDataTableConverter
    {
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
