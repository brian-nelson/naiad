﻿using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Exceptions.DataManagement;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class RelationshipRepo : IRelationshipRepo
{
    private readonly BaseRepo<Relationship> _repo;

    public RelationshipRepo(
        ILiteDatabase database)
    {
        _repo = new BaseRepo<Relationship>(database, "relationships");

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
        var existing = GetRelationship(
            relationship.ParentId,
            relationship.ChildId,
            relationship.ConnectionContext);

        if (existing != null
            && existing.Id != relationship.Id)
        {
            throw new DuplicateKeyViolation("Record already exists with this ParentId, ChildId and connectionContext");
        }

        _repo.Save(relationship);
    }

    public IEnumerable<Relationship> GetAll()
    {
        return _repo.GetAll();
    }

    public Relationship GetRelationship(
        Guid parentId, 
        Guid childId,
        string connectionContext)
    {
        return _repo.GetItem(Query.And(
            Query.EQ("ParentId", parentId),
            Query.EQ("ChildId", childId),
            Query.EQ("ConnectionContext", connectionContext)));
    }
}

