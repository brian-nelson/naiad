using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class DataPointerRepo : IDataPointerRepo
{
    private readonly ILiteCollection<DataPointer> _collection;

    public DataPointerRepo(
        ILiteDatabase database)
    {
        _collection = database.GetCollection<DataPointer>("datapointers");

        _collection.EnsureIndex(x => x.GranularityId);
        _collection.EnsureIndex(x => x.ZoneId);
    }

    public DataPointer GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Save(DataPointer pointer)
    {
        throw new NotImplementedException();
    }
}

