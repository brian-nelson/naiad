using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System;

public class UserAccessRepo : IUserAccessRepo
{
    private readonly BaseRepo<UserAccess> _repo;

    public UserAccessRepo(ILiteDatabase database)
    {
        _repo = new BaseRepo<UserAccess>(database, "useraccesses");

        _repo.EnsureIndex(x => x.UserId, true);
    }

    public UserAccess GetById(Guid userAccessId)
    {
        return _repo.GetById(userAccessId);
    }

    public UserAccess GetByUserId(Guid userId)
    {
        return _repo.GetItem(Query.EQ("UserId", userId));
    }

    public void Save(UserAccess userAccess)
    {
        _repo.Save(userAccess);
    }

    public IEnumerable<UserAccess> GetAll()
    {
        return _repo.GetAll();
    }
}
