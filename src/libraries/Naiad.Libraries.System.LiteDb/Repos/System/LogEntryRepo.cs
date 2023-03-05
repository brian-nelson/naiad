using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System;

public class LogEntryRepo : ILogEntryRepo
{
    private readonly BaseRepo<LogEntry> _repo;

    public LogEntryRepo(ILiteDatabase database)
    {
        _repo = new BaseRepo<LogEntry>(database, "logentries");

        _repo.EnsureIndex(x => x.EntryDateTime, false);
        _repo.EnsureIndex(x => x.UserId, false);
    }

    public LogEntry GetById(Guid logEntryId)
    {
        return _repo.GetById(logEntryId);
    }

    public IEnumerable<LogEntry> GetByUser(Guid userId, DateTime startDate, DateTime endDate)
    {
        return _repo.GetItems(
            Query.And(
                Query.EQ("UserId", userId),
                Query.GTE("EntryDateTime", startDate),
                Query.LTE("EntryDateTime", endDate)));
    }

    public IEnumerable<LogEntry> Get(DateTime startDate, DateTime endDate)
    {
        return _repo.GetItems(
            Query.And(
                Query.GTE("EntryDateTime", startDate),
                Query.LTE("EntryDateTime", endDate)));
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

    public IEnumerable<LogEntry> GetAll()
    {
        return _repo.GetAll();
    }
}