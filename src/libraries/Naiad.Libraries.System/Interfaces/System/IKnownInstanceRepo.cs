using Naiad.Libraries.System.Models.System;
using System;
using System.Collections.Generic;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IKnownInstanceRepo
{
    public KnownInstance GetById(Guid knownInstanceId);
    public IEnumerable<KnownInstance> GetAll();
    public void Save(KnownInstance knownInstance);
}