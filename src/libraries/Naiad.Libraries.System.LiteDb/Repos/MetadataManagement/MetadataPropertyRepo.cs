using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class MetadataPropertyRepo : IMetadataPropertyRepo
{
    private readonly BaseRepo<MetadataProperty> _repo;

    public MetadataPropertyRepo(
        ILiteDatabase database)
    {
        _repo = new BaseRepo<MetadataProperty>(database, "metadataproperties");

        _repo.EnsureIndex(x => x.MetadataId, false);
        _repo.EnsureIndex(x => x.Key, false);
    }

    public MetadataProperty GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public void Save(MetadataProperty property)
    {
        _repo.Save(property);
    }

    public IEnumerable<MetadataProperty> GetAll()
    {
        return _repo.GetAll();
    }

    public IEnumerable<MetadataProperty> Get(Guid metadataId)
    {
        return _repo.GetItems(Query.EQ("MetadataId", metadataId));
    }

    public IEnumerable<MetadataProperty> GetByKey(string key)
    {
        return _repo.GetItems(Query.EQ("Key", key));
    }

    public MetadataProperty GetByIdAndKey(Guid metadataId, string key)
    {
        return _repo.GetItem(Query.And(
            Query.EQ("MetadataId", metadataId),
            Query.EQ("Key", key)));
    }
}
