using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class RelationshipRepo : IRelationshipRepo
{
    private readonly InternalRepo<Relationship> _repo;

    public RelationshipRepo(
        ILiteDatabase database)
    {
        _repo = new InternalRepo<Relationship>(database, "relationships");

        _repo.EnsureIndex(x => x.ParentId);
        _repo.EnsureIndex(x => x.ChildId);
    }

    public Relationship GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public IEnumerable<Relationship> GetChildren(Guid parentId)
    {
        return _repo.GetItems(Query.EQ("ParentId", parentId));
    }

    public IEnumerable<Relationship> GetParents(Guid childId)
    {
        return _repo.GetItems(Query.EQ("ChildId", childId));
    }

    public void Save(Relationship relationship)
    {
        _repo.Save(relationship);
    }
}

