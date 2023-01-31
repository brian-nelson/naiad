using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class CategorizationRepo : ICategorizationRepo
{
    private readonly ILiteCollection<Categorization> _collection;

    public CategorizationRepo(
        ILiteDatabase database)
    {
        _collection = database.GetCollection<Categorization>("categorizations");
    }


    public IEnumerable<Categorization> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Save(Categorization zone)
    {
        throw new NotImplementedException();
    }

    public Categorization GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}

