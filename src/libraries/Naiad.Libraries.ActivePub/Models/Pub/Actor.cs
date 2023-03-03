using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Naiad.Libraries.ActivityPub.Models.Pub;

public class Actor
{
    [JsonProperty("@context")] 
    public IEnumerable<object> Context { get; set; }

    public Uri Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string PreferredUsername { get; set; }
    public string Summary { get; set; }
    public Uri Inbox { get; set; }
    public Uri Outbox { get; set; }
    public Uri Followers { get; set; }
    public Uri Following { get; set; }
    public object Icon { get; set; }
    public PublicKeyAP PublicKey { get; set; }
    public Endpoints Endpoints { get; set; }
}