using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class MetadataRepo : IMetadataRepo
{
    private readonly ILiteCollection<Metadata> _collection;

    public MetadataRepo(
        ILiteDatabase database)
    {
        _collection = database.GetCollection<Metadata>("metadatas");

        _collection.EnsureIndex(x => x.CategorizationId);
    }

    public Metadata GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Save(Metadata metadata)
    {
        throw new NotImplementedException();
    }
}

