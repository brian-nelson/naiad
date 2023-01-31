using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

internal class GranularityRepo : IGranularityRepo
{
    private readonly ILiteCollection<Granularity> _collection;

    public GranularityRepo(
        ILiteDatabase database)
    {
        _collection = database.GetCollection<Granularity>("granularities");
    }

    public IEnumerable<Granularity> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Save(Granularity zone)
    {
        throw new NotImplementedException();
    }

    public Granularity GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}

