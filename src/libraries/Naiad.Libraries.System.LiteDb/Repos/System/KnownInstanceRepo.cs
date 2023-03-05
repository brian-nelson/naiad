using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;
using System.Collections.Generic;
using System;

namespace Naiad.Libraries.System.LiteDb.Repos.System;

public class KnownInstanceRepo : IKnownInstanceRepo
{
    private readonly BaseRepo<KnownInstance> _repo;

    public KnownInstanceRepo(ILiteDatabase database)
    {
        _repo = new BaseRepo<KnownInstance>(database, "knowninstances");
    }

    public KnownInstance GetById(Guid knownInstanceId)
    {
        return _repo.GetById(knownInstanceId);
    }
    
    public IEnumerable<KnownInstance> GetAll()
    {
        return _repo.GetAll();
    }

    public void Save(KnownInstance knownInstance)
    {
        _repo.Save(knownInstance);
    }
}
