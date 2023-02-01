using Naiad.Libraries.System.Models.MetadataManagement;
using System;
using System.Collections.Generic;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface IGranularityRepo
{
    public IEnumerable<Granularity> GetAll();
    public void Save(Granularity granularity);
    public Granularity GetById(Guid id);
}

