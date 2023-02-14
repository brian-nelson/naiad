using System;

namespace Naiad.Libraries.System.Models.MetadataManagement;

public class Metadata
    : AbstractDbRecord
{
    public Guid? CategorizationId { get; set; }
}