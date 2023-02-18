using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class CategorizationRepo : ICategorizationRepo
{
    private readonly InternalRepo<Categorization> _repo;

    public CategorizationRepo(
        ILiteDatabase database)
    {
        _repo = new InternalRepo<Categorization>(database, "categorizations");
    }

    public IEnumerable<Categorization> GetAll()
    {
        return _repo.GetAll();
    }

    public void Save(Categorization categorization)
    {
        _repo.Save(categorization);
    }

    public Categorization GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public Categorization GetByName(string name)
    {
        return _repo.GetItem(Query.EQ("Name", name));
    }
}

