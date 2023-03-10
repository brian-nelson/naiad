using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System
{
    public class SystemEventRepo 
        : ISystemEventRepo
    {
        private readonly BaseRepo<SystemEvent> _repo;

        public SystemEventRepo(ILiteDatabase database)
        {
            _repo = new BaseRepo<SystemEvent>(database, "systemevents");
            _repo.EnsureIndex(x => x.Time, false);
        }

        public SystemEvent GetById(Guid id)
        {
            return _repo.GetById(id);
        }

        public void Save(SystemEvent obj)
        {
            _repo.Save(obj);
        }

        public IEnumerable<SystemEvent> GetAll()
        {
            return _repo.GetAll();
        }

        public IEnumerable<SystemEvent> GetByDate(DateTime startDateTime, DateTime endDateTime)
        {
            return _repo.GetItems(Query.Between("Time", startDateTime, endDateTime));
        }
    }
}
