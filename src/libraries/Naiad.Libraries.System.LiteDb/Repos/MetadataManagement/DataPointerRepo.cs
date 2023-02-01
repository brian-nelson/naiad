using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class DataPointerRepo : IDataPointerRepo
{
    private readonly InternalRepo<DataPointer> _repo;

    public DataPointerRepo(
        ILiteDatabase database)
    {
        _repo = new InternalRepo<DataPointer>(database, "datapointers");

        _repo.EnsureIndex(x => x.GranularityId);
        _repo.EnsureIndex(x => x.ZoneId);
    }

    public DataPointer GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public void Save(DataPointer pointer)
    {
        _repo.Save(pointer);
    }
}

