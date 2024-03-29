﻿using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System;

public class ConfigurationRepo : IConfigurationRepo
{
    private readonly BaseRepo<Configuration> _repo;

    public ConfigurationRepo(ILiteDatabase database)
    {
        _repo = new BaseRepo<Configuration>(database, "configurations");

        _repo.EnsureIndex(x => x.Key, true);
    }

    public Configuration GetById(Guid configurationId)
    {
        return _repo.GetById(configurationId);
    }

    public Configuration GetByKey(string key)
    {
        return _repo.GetItem(Query.EQ("Key", key));
    }

    public IEnumerable<Configuration> GetAllExternal()
    {
        return _repo.GetItems(Query.EQ("IsInternal", false));
    }

    public IEnumerable<Configuration> GetAllInternal()
    {
        return _repo.GetItems(Query.EQ("IsInternal", true));
    }

    public IEnumerable<Configuration> GetAll()
    {
        return _repo.GetAll();
    }

    public void Save(Configuration configuration)
    {
        _repo.Save(configuration);
    }
}
