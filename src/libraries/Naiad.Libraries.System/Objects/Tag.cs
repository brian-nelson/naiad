using System;
using Naiad.Libraries.System.Interfaces;

namespace Naiad.Libraries.System.Objects
{
    public class Tag : IDbRecord
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
