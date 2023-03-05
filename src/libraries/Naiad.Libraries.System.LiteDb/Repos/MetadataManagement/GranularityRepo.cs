using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

internal class GranularityRepo : IGranularityRepo
{
    private readonly BaseRepo<Granularity> _repo;

    public GranularityRepo(
        ILiteDatabase database)
    {
        _repo = new BaseRepo<Granularity>(database, "granularities");

        _repo.EnsureIndex(x => x.Name, true);
    }

    public IEnumerable<Granularity> GetAll()
    {
        return _repo.GetAll();
    }

    public void Save(Granularity granularity)
    {
        _repo.Save(granularity);
    }

    public Granularity GetById(Guid id)
    {
        return _repo.GetById(id);
    }
}

