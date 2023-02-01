using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System
{
    public class LogEntryRepo : ILogEntryRepo
    {
        private readonly InternalRepo<LogEntry> _repo;

        public LogEntryRepo(ILiteDatabase database)
        {
            _repo = new InternalRepo<LogEntry>(database, "logentries");

            _repo.EnsureIndex(x => x.EntryDateTime, true);
            _repo.EnsureIndex(x => x.UserId, false);
        }

        public LogEntry GetById(Guid logEntryId)
        {
            return _repo.GetById(logEntryId);
        }

        public LogEntry GetByUser(Guid userId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<LogEntry> logEntries)
        {
            foreach (var logEntry in logEntries)
            {
                Save(logEntry);
            }
        }

        public void Save(LogEntry logEntry)
        {
            _repo.Save(logEntry);
        }
    }
}
