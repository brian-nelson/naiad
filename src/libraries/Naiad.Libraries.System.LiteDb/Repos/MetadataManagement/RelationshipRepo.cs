using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class RelationshipRepo : IRelationshipRepo
{
    private readonly ILiteCollection<Relationship> _collection;

    public RelationshipRepo(
        ILiteDatabase database)
    {
        _collection = database.GetCollection<Relationship>("relationships");

        _collection.EnsureIndex(x => x.ParentId);
        _collection.EnsureIndex(x => x.ChildId);
    }

    public Relationship GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Relationship> GetChildren(Guid parentId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Relationship> GetParents(Guid childId)
    {
        throw new NotImplementedException();
    }

    public void Save(Relationship relationship)
    {
        throw new NotImplementedException();
    }
}

