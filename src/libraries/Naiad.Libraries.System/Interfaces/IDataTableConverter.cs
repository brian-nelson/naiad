﻿using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Naiad.Libraries.System.Interfaces
{
    public interface IDataTableConverter
    {
        public string Name { get; }

        public DataTable Convert(Stream sourceFile);

        public IEnumerable<string> SupportedMimeTypes { get; }
    }
}
