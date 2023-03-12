using System.Collections.Generic;
using System.IO;
using System.Threading;
using MastodonTestClient.Client;
using MastodonTestClient.Helpers;
using MastodonTestClient.Objects;
using Naiad.Libraries.Core.Helpers;

namespace MastodonTestClient;

class Program
{

    static void Main(string[] args)
    {
        string inputFile = args[0];
        string outputFile = args[1];

        var usernames = File.ReadAllLines(inputFile);

        MastodonClient mastodon = new MastodonClient();

        var output = new List<Post>();

        foreach (var username in usernames)
        {
            if (!username.IsNullOrEmpty())
            {
                var serverName = MastodonClient.GetServerName(username);
                var accountName = MastodonClient.GetAccountName(username);

                var accounts = mastodon.Search(serverName, accountName);

                var account = accounts.FindAccount(serverName, accountName);

                if (account != null)
                {
                    var statuses = mastodon.ListStatuses(serverName, account.Id);

                    foreach (var status in statuses)
                    {
                        var post = new Post
                        {
                            Id = status.Id,
                            Username = username,
                            CreatedOn = status.CreatedAt,
                            Content = status.Content
                        };

                        if (!post.Content.IsNullOrEmpty())
                        {
                            output.Add(post);
                        }
                    }
                }
            }

            Thread.Sleep(10000);
        }

        output.WriteToCsv(outputFile);
    }
}