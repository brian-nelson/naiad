using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System;

public class ConnectorConfigurationRepo : IConnectorConfigurationRepo
{
    private readonly BaseRepo<ConnectorConfiguration> _repo;

    public ConnectorConfigurationRepo(ILiteDatabase database)
    {
        _repo = new BaseRepo<ConnectorConfiguration>(database, "connectorconfigs");
    }

    public ConnectorConfiguration GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public void Save(ConnectorConfiguration obj)
    {
        _repo.Save(obj);
    }

    public IEnumerable<ConnectorConfiguration> GetAll()
    {
        return _repo.GetAll();
    }
}