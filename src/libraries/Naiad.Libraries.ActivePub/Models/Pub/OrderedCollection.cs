using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Naiad.Libraries.ActivePub.Models.Pub;

public class OrderedCollection<T>
{
    [JsonProperty("@context")] 
    public string Context { get; set; } = "https://www.w3.org/ns/activitystreams";
    
    [JsonProperty("summary")] 
    public string Summary { get; set; }

    [JsonProperty("type")] 
    public string Type { get; set; } = "OrderedCollection";

    [JsonProperty("totalItems")] 
    public int TotalItems => OrderedItems.Count();

    [JsonProperty("orderedItems")] 
    public IEnumerable<T> OrderedItems { get; set; }

}