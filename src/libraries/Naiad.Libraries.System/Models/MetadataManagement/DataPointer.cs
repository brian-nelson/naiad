using System;

namespace Naiad.Libraries.System.Models.MetadataManagement;

public class DataPointer 
    : AbstractDbRecord
{
    public DataPointer()
    {
        StorageLocation = null;
        GranularityId = null;
        ZoneId = null;
    }

    public string StorageLocation { get; set; }

    public Guid? GranularityId { get; set; }

    public Guid? ZoneId { get; set; }
}
