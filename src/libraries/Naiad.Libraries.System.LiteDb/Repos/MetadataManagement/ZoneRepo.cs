using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class ZoneRepo : IZoneRepo
{
    private readonly ILiteCollection<Zone> _collection;

    public ZoneRepo(
        ILiteDatabase database)
    {
        _collection = database.GetCollection<Zone>("zones");
    }

    public IEnumerable<Zone> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Save(Zone zone)
    {
        throw new NotImplementedException();
    }

    public Zone GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}

