using Naiad.Libraries.System.Models.System;
using System;
using System.Collections.Generic;
using Naiad.Libraries.Core.Interfaces;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IAccessKeyRepo : IDataRepository<AccessKey>
{
    public IEnumerable<AccessKey> GetByUserId(Guid userId);

    public AccessKey GetByKey(string key);
}