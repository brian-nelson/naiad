using System.Collections.Generic;
using MastodonTestClient.Models;

namespace MastodonTestClient.Helpers
{
    public static class AccountHelper
    {
        public static Account FindAccount(
            this IEnumerable<Account> accounts,
            string serverName, 
            string accountName)
        {
            var url = $"https://{serverName}/@{accountName}";

            foreach (var account in accounts)
            {
                if (account.Url.OriginalString.Equals(url))
                {
                    return account;
                }
            }

            return null;
        }
    }
}
