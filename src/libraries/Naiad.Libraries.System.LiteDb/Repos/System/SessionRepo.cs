using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System;

public class SessionRepo : ISessionRepo
{
    private readonly InternalRepo<Session> _repo;

    public SessionRepo(ILiteDatabase database)
    {
        _repo = new InternalRepo<Session>(database, "sessions");

        _repo.EnsureIndex(x => x.UserId, false);
    }

    public Session GetById(Guid sessionId)
    {
        return _repo.GetById(sessionId);
    }

    public IEnumerable<Session> GetByEmail(string email)
    {
        return _repo.GetItems(
            Query.And(
                Query.EQ("Email", email),
                Query.EQ("IsDeleted", false)));
    }

    public void Save(Session session)
    {
        _repo.Save(session);
    }
}
