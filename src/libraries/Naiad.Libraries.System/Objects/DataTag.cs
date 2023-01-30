using System;
using Naiad.Libraries.System.Interfaces;

namespace Naiad.Libraries.System.Objects
{
    public class DataTag : IDbRecord
    {
        public Guid Id { get; set; }

        public Guid DataIndexId { get; set; }
        
        public string Tag { get; set; }
    }
}
