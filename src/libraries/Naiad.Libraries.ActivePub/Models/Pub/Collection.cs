using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Naiad.Libraries.ActivePub.Models.Pub;

public class Collection<T>
{
    [JsonProperty("@context")] 
    public string Context { get; set; } = "https://www.w3.org/ns/activitystreams";
    
    [JsonProperty("summary")] 
    public string Summary { get; set; }
    
    [JsonProperty("type")] 
    public string Type { get; set; } = "Collection";
    
    [JsonProperty("totalItems")] 
    public int TotalItems => Items.Count();
    
    [JsonProperty("items")] 
    public IEnumerable<T> Items { get; set; }
}

