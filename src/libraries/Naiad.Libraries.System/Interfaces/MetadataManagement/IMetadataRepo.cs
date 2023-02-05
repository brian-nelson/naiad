using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface IMetadataRepo
{
    public Metadata GetById(Guid id);
    public void Save(Metadata metadata);
    public IEnumerable<Metadata> Get(Guid categorizationId);
}