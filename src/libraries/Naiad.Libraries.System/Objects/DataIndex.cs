using System;

using Naiad.Libraries.System.Interfaces;

namespace Naiad.Libraries.System.Objects
{
    public class DataIndex : IDbRecord
    {
        public Guid Id { get; set; }
        
        public string SourceName { get; set; }


    }
}
