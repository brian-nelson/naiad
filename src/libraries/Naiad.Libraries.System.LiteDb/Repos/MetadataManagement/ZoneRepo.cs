using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class ZoneRepo : IZoneRepo
{
    private readonly InternalRepo<Zone> _repo;

    public ZoneRepo(
        ILiteDatabase database)
    {
        _repo = new InternalRepo<Zone>(database, "zones");

        _repo.EnsureIndex(x => x.Name, true);
    }

    public IEnumerable<Zone> GetAll()
    {
        return _repo.GetAll();
    }

    public void Save(Zone zone)
    {
        _repo.Save(zone);
    }

    public Zone GetById(Guid id)
    {
        return _repo.GetById(id);
    }
}

