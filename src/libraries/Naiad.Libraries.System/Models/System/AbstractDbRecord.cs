using System;
using Naiad.Libraries.System.Interfaces.System;

namespace Naiad.Libraries.System.Models.System
{
    public abstract class AbstractDbRecord 
        : IDbRecord
    {
        protected AbstractDbRecord()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
