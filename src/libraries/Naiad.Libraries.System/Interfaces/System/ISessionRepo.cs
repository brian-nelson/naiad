using Naiad.Libraries.System.Models.System;
using System;
using System.Collections.Generic;

namespace Naiad.Libraries.System.Interfaces.System;

public interface ISessionRepo : IDataRepository<Session>
{
    public IEnumerable<Session> GetByUser(Guid userId);
}