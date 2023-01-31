using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface IZoneRepo
{
    public IEnumerable<Zone> GetAll();
    public void Save(Zone zone);
    public Zone GetById(Guid id);
}