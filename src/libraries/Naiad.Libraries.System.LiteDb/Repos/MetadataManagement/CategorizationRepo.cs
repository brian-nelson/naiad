using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Exceptions.DataManagement;
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

        _repo.EnsureIndex(x => x.Name, true);
    }

    public IEnumerable<Categorization> GetAll()
    {
        return _repo.GetAll();
    }

    public void Save(Categorization categorization)
    {
        var existing = GetByName(categorization.Name);
        if (existing != null
            && !existing.Id.Equals(categorization.Id))
        {
            throw new DuplicateKeyViolation("Record already exists given categorization name");
        }

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

