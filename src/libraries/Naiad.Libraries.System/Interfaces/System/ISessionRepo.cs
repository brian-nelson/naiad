using Naiad.Libraries.System.Models.System;
using System;
using System.Collections.Generic;

namespace Naiad.Libraries.System.Interfaces.System;

public interface ISessionRepo
{
    public Session GetById(Guid sessionId);
    public IEnumerable<Session> GetByUser(Guid userId);
    public void Save(Session session);
}