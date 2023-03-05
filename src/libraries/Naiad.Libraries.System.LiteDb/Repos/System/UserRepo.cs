using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Constants.System;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System;

public class UserRepo : IUserRepo
{
    private readonly BaseRepo<User> _repo;

    public UserRepo(ILiteDatabase database)
    {
        _repo = new BaseRepo<User>(database, "users");

        _repo.EnsureIndex(x => x.Email, true);
        //_repo.EnsureIndex(x => x.Username, true);
    }

    public User GetById(Guid userId)
    {
        return _repo.GetById(userId);
    }

    public IEnumerable<User> GetAll()
    {
        return _repo.GetAll();
    }

    public User GetByEmail(string email)
    {
        return _repo.GetItem(Query.EQ("Email", email));
    }

    public User GetByUsername(string username)
    {
        return _repo.GetItem(Query.EQ("Username", username));
    }

    public void Save(User user)
    {
        _repo.Save(user);
    }

    public IEnumerable<User> GetByType(UserTypes userType)
    {
        return _repo.GetItems(Query.EQ("UserType", Enum.GetName(typeof(UserTypes), userType)));
    }
}
