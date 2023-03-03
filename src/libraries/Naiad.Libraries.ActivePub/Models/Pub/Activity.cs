using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace Naiad.Libraries.ActivePub.Models.Pub;

public class Activity
{
    [JsonProperty("@context")]
    public object Context { get; set; } = new List<object>
    {
        "https://www.w3.org/ns/activitystreams"
    };

    [JsonProperty("id")] 
    public Uri Id { get; set; }

    [JsonProperty("type")] 
    public string Type { get; set; }
    
    [JsonProperty("actor")] 
    public Uri Actor { get; set; }
    
    [JsonProperty("object")] 
    public object Object { get; set; }
    
    [JsonProperty("to")] 
    public IEnumerable<string> To { get; set; }
    
    [JsonProperty("bto")] 
    public IEnumerable<string> Bto { get; set; }
    
    [JsonProperty("cc")] 
    public IEnumerable<string> Cc { get; set; }
    
    [JsonProperty("bcc")] 
    public IEnumerable<string> Bcc { get; set; }
    
    [JsonProperty("audience")] 
    public IEnumerable<string> Audience { get; set; }
}
