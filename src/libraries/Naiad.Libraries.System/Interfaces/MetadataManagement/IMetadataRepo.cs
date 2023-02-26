using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface IMetadataRepo : IDataRepository<Metadata>
{
    public IEnumerable<Metadata> Get(Guid categorizationId);
}