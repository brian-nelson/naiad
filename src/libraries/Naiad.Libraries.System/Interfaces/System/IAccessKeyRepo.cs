using Naiad.Libraries.System.Models.System;
using System;
using System.Collections.Generic;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IAccessKeyRepo
{
    public AccessKey GetById(Guid accessKeyId);
    public IEnumerable<AccessKey> GetByUserId(Guid userId);
    public void Save(AccessKey accessKey);
}