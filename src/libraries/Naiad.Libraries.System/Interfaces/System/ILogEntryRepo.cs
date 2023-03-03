using Naiad.Libraries.System.Models.System;
using System;
using System.Collections.Generic;
using Naiad.Libraries.Core.Interfaces;

namespace Naiad.Libraries.System.Interfaces.System;

public interface ILogEntryRepo : IDataRepository<LogEntry>
{
    public IEnumerable<LogEntry> GetByUser(Guid userId, DateTime startDate, DateTime endDate);
    public IEnumerable<LogEntry> Get(DateTime startDate, DateTime endDate);
    public void Save(IEnumerable<LogEntry> logEntries);
}