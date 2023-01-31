using System;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface IDataPointerRepo
{
    public DataPointer GetById(Guid id);
    public void Save(DataPointer pointer);
}
