using Newtonsoft.Json;
using System;

namespace MastodonTestClient.Models;

public class Status
{
    [JsonProperty("id")] 
    public string Id { get; set; }

    [JsonProperty("created_at")] 
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("in_reply_to_id")]
    public string InReplyToId { get; set; }

    [JsonProperty("in_reply_to_account_id")]
    public string InReplyToAccountId { get; set; }

    [JsonProperty("sensitive")] 
    public bool Sensitive { get; set; }

    [JsonProperty("spoiler_text")] 
    public string SpoilerText { get; set; }

    [JsonProperty("visibility")] 
    public string Visibility { get; set; }

    [JsonProperty("language")] 
    public string Language { get; set; }

    [JsonProperty("uri")] 
    public Uri Uri { get; set; }

    [JsonProperty("url")]
    public Uri Url { get; set; }

    [JsonProperty("replies_count")] 
    public int RepliesCount { get; set; }

    [JsonProperty("reblogs_count")] 
    public int ReblogsCount { get; set; }

    [JsonProperty("favourites_count")]
    public int FavouritesCount { get; set; }

    [JsonProperty("edited_at")]
    public DateTimeOffset? EditedAt { get; set; }

    [JsonProperty("local_only")]
    public bool LocalOnly { get; set; }

    [JsonProperty("content")] 
    public string Content { get; set; }

    // TODO Deal with additional
    // "reblog": null,
    // "account": {}
    // "media_attachments":[]
    // "mentions" : []
    // "tags": [],
    // "emojis": [],
    // "card" : {}
    // "poll" : {}
}