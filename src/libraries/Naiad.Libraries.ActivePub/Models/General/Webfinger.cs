using System;
using System.Collections.Generic;
using Naiad.Libraries.Core.Interfaces;
using Newtonsoft.Json;

namespace Naiad.Libraries.ActivityPub.Models.General;

public class Webfinger : IDbRecord
{
    public Webfinger()
    {
        Id = Guid.NewGuid();
    }

    [JsonIgnore]
    public Guid Id { get; set; }

    public string Subject { get; set; }
    public IEnumerable<Link> Links { get; set; }
    
}