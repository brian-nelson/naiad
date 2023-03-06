﻿using Naiad.Libraries.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Naiad.Libraries.ActivityPub.Models.Pub;

public class Post : IDbRecord
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonPropertyName("to")] 
    public IEnumerable<string> To { get; set; }
    
    [JsonPropertyName("name")] 
    public string Name { get; set; }
    
    [JsonPropertyName("summary")] 
    public string Summary { get; set; }
    
    [JsonPropertyName("sensitive")] 
    public bool? Sensitive { get; set; }
    
    [JsonPropertyName("inReplyTo")] 
    public Uri InReplyTo { get; set; }
    
    [JsonPropertyName("content")] 
    public string Content { get; set; }
    
    [JsonPropertyName("id")] 
    public Uri IdUri { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("published")] 
    public DateTime Published { get; set; }
    
    [JsonPropertyName("attributedTo")] 
    public Uri AttributedTo { get; set; }

    // Likes and Shares are not used like this by Mastodon and Pixelfed...
    // Keep this in mind.
    [JsonPropertyName("likes")] 
    public Uri Likes { get; set; }
    
    [JsonPropertyName("shares")] 
    public Uri Shares { get; set; }

}