using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System;

public class AccessKeyRepo : IAccessKeyRepo
{
    private readonly BaseRepo<AccessKey> _repo;

    public AccessKeyRepo(ILiteDatabase database)
    {
        _repo = new BaseRepo<AccessKey>(database, "accesskeys");
        _repo.EnsureIndex(x => x.UserId, false);
        _repo.EnsureIndex(x => x.Key, true);
    }

    public AccessKey GetByKey(string key)
    {
        return _repo.GetItem(Query.EQ("Key", key));
    }

    public AccessKey GetById(Guid accessKeyId)
    {
        return _repo.GetById(accessKeyId);
    }

    public IEnumerable<AccessKey> GetByUserId(Guid userId)
    {
        return _repo.GetItems(Query.EQ("UserId", userId));
    }

    public void Save(AccessKey accessKey)
    {
        _repo.Save(accessKey);
    }

    public IEnumerable<AccessKey> GetAll()
    {
        return _repo.GetAll();
    }
}
