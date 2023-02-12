using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface IDataPointerRepo
{
    public DataPointer GetById(Guid id);
    public DataPointer GetByLocation(string fileId);

    public void Save(DataPointer pointer);

    public IEnumerable<DataPointer> GetByZone(Guid zoneId);
    public IEnumerable<DataPointer> GetByGranularity(Guid granularityId);
    public IEnumerable<DataPointer> GetByZoneAndGranularity(Guid zoneId, Guid granularityId);
}
