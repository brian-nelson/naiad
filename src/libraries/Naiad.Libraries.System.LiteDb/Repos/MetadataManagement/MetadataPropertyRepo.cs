using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class MetadataPropertyRepo : IMetadataPropertyRepo
{
    private readonly ILiteCollection<MetadataProperty> _collection;

    public MetadataPropertyRepo(
        ILiteDatabase database)
    {
        _collection = database.GetCollection<MetadataProperty>("metadataproperties");

        _collection.EnsureIndex(x => x.MetadataId);
    }

    public MetadataProperty GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Save(MetadataProperty property)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MetadataProperty> Get(Guid metadataId)
    {
        throw new NotImplementedException();
    }
}
