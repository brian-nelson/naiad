using System.Collections.Generic;

namespace Naiad.Libraries.System.Models.System;

public class HandlerConfiguration
    : AbstractDbRecord
{
    public HandlerConfiguration()
    {
        Configuration = new Dictionary<string, string>();
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public string ConnectorType { get; set; }

    public Dictionary<string, string> Configuration { get; set; }
}