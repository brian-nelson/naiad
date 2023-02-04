using System;

namespace Naiad.Libraries.System.Models.MetadataManagement;

public class MetadataProperty
    : AbstractDbRecord
{
    public Guid MetadataId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}
