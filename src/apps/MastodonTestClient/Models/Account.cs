using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MastodonTestClient.Models;

public class Account
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("acct")]
    public string Name { get; set; }

    [JsonProperty("display_name")]
    public string DisplayName { get; set; }

    [JsonProperty("locked")]
    public bool Locked { get; set; }

    [JsonProperty("bot")]
    public bool Bot { get; set; }

    [JsonProperty("discoverable")]
    public bool Discoverable { get; set; }

    [JsonProperty("group")]
    public bool Group { get; set; }

    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("note")]
    public string Note { get; set; }

    [JsonProperty("url")]
    public Uri Url { get; set; }

    [JsonProperty("avatar")]
    public Uri Avatar { get; set; }

    [JsonProperty("avatar_static")]
    public Uri AvatarStatic { get; set; }

    [JsonProperty("header")]
    public Uri Header { get; set; }

    [JsonProperty("header_static")]
    public Uri HeaderStatic { get; set; }

    [JsonProperty("followers_count")]
    public int FollowersCount { get; set; }

    [JsonProperty("following_count")]
    public int FollowingCount { get; set; }

    [JsonProperty("statuses_count")]
    public int StatusesCount { get; set; }

    [JsonProperty("last_status_at")]
    public DateTimeOffset? LastStatusAt { get; set; }

    [JsonProperty("noindex")]
    public bool NoIndex { get; set; }

    // TODO Deal with remaining children
    // "emojis": [],
    // "roles": [],
    // "fields": []
}
