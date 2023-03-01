using System;
using System.Collections.Generic;

namespace Naiad.Modules.Api.Core.Objects;

public class StructuredDataDetailDto
{
    public StructuredDataDetailDto()
    {
        Columns = new List<string>();
    }

    public Guid MetadataId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string MimeType { get; set; }

    public string IdentifierName { get; set; }

    public int RowCount { get; set; }

    public List<string> Columns { get; set; }
}
