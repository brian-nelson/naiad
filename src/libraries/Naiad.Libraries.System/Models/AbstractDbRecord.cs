using System;
using Naiad.Libraries.Core.Interfaces;

namespace Naiad.Libraries.System.Models;

public abstract class AbstractDbRecord
    : IDbRecord
{
    protected AbstractDbRecord()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
}

