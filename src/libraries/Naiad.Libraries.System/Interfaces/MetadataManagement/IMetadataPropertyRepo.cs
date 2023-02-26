using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface IMetadataPropertyRepo : IDataRepository<MetadataProperty>
{
    public IEnumerable<MetadataProperty> Get(Guid metadataId);
    public IEnumerable<MetadataProperty> GetByKey(string key);
    public MetadataProperty GetByIdAndKey(Guid metadataId, string key);
}