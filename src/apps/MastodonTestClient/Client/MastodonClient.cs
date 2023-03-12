using System.Collections.Generic;
using System.Net.Http;
using MastodonTestClient.Models;
using Naiad.Libraries.Core.Helpers;

namespace MastodonTestClient.Client;

public class MastodonClient
{
    private readonly HttpClient _httpClient;

    public MastodonClient()
    {
        _httpClient = new HttpClient();
    }

    public static string GetServerName(string user)
    {
        var parts = user.Split("@");

        if (parts.Length > 1)
        {
            return parts[1];
        }

        return null;
    }

    public static string GetAccountName(string user)
    {
        var parts = user.Split("@");

        if (parts.Length > 1)
        {
            return parts[0];
        }

        return null;
    }

    public IEnumerable<Account> Search(string serverName, string accountName)
    {
        var url = $"https://{serverName}/api/v2/search?q={accountName}";

        var responseTask = _httpClient.GetAsync(url);
        var response = responseTask.Result;

        var outputTask = response.Content.ReadAsStringAsync();
        var outputJson = outputTask.Result;

        var searchAccountsResponse = JsonHelper.ToObject<SearchAccountsResponse>(outputJson);
        return searchAccountsResponse.Accounts;
    }

    public IEnumerable<Status> ListStatuses(string serverName, string accountId)
    {
        var url = $"https://{serverName}/api/v1/accounts/{accountId}/statuses";

        var responseTask = _httpClient.GetAsync(url);
        var response = responseTask.Result;

        var outputTask = response.Content.ReadAsStringAsync();
        var outputJson = outputTask.Result;

        var statuses = JsonHelper.ToObject<List<Status>>(outputJson);
        return statuses;
    }
}