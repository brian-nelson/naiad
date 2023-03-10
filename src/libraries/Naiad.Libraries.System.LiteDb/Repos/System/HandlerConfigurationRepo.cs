using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System;

public class HandlerConfigurationRepo : IHandlerConfigurationRepo
{
    private readonly BaseRepo<HandlerConfiguration> _repo;

    public HandlerConfigurationRepo(ILiteDatabase database)
    {
        _repo = new BaseRepo<HandlerConfiguration>(database, "connectorconfigs");
    }

    public HandlerConfiguration GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public void Save(HandlerConfiguration obj)
    {
        _repo.Save(obj);
    }

    public IEnumerable<HandlerConfiguration> GetAll()
    {
        return _repo.GetAll();
    }
}