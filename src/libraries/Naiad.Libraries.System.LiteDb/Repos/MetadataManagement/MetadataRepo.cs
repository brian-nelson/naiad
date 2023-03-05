using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class MetadataRepo : IMetadataRepo
{
    private readonly BaseRepo<Metadata> _repo;

    public MetadataRepo(
        ILiteDatabase database)
    {
        _repo = new BaseRepo<Metadata>(database, "metadata");

        _repo.EnsureIndex(x => x.CategorizationId);
    }

    public Metadata GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public void Save(Metadata metadata)
    {
        _repo.Save(metadata);
    }

    public IEnumerable<Metadata> GetAll()
    {
        return _repo.GetAll();
    }

    public IEnumerable<Metadata> Get(Guid categorizationId)
    {
        return _repo.GetItems(Query.EQ("CategorizationId", categorizationId));
    }
}

