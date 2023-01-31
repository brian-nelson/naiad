using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface IMetadataPropertyRepo
{
    public MetadataProperty GetById(Guid id);
    public void Save(MetadataProperty property);
    public IEnumerable<MetadataProperty> Get(Guid metadataId);
}