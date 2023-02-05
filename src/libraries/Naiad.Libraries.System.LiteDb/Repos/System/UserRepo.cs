using System;
using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System;

public class UserRepo : IUserRepo
{
    private readonly InternalRepo<User> _repo;

    public UserRepo(ILiteDatabase database)
    {
        _repo = new InternalRepo<User>(database, "users");

        _repo.EnsureIndex(x => x.Email, true);
    }

    public User GetById(Guid userId)
    {
        return _repo.GetById(userId);
    }

    public User GetByEmail(string email)
    {
        return _repo.GetItem(Query.EQ("Email", email));
    }

    public void Save(User user)
    {
        _repo.Save(user);
    }

}
