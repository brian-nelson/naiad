using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class MetadataPropertyRepo : IMetadataPropertyRepo
{
    private readonly InternalRepo<MetadataProperty> _repo;

    public MetadataPropertyRepo(
        ILiteDatabase database)
    {
        _repo = new InternalRepo<MetadataProperty>(database, "metadataproperties");

        _repo.EnsureIndex(x => x.MetadataId);
    }

    public MetadataProperty GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public void Save(MetadataProperty property)
    {
        _repo.Save(property);
    }

    public IEnumerable<MetadataProperty> Get(Guid metadataId)
    {
        return _repo.GetItems(Query.EQ("MetadataId", metadataId));
    }
}
