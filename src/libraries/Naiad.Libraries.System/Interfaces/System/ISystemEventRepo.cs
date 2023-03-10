using System;
using System.Collections.Generic;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.Interfaces.System;

public interface ISystemEventRepo 
    : IDataRepository<SystemEvent>
{
    public IEnumerable<SystemEvent> GetByDate(DateTime startDateTime, DateTime endDateTime);
}