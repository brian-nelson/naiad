using System.Collections.Generic;
using Newtonsoft.Json;

namespace MastodonTestClient.Models
{
    public class SearchAccountsResponse
    {
        [JsonProperty("accounts")]
        public List<Account> Accounts { get; set; }
    }
}
