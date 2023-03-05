using System;
using System.Collections.Generic;
using Naiad.Libraries.ActivityPub.Models.General;

namespace Naiad.Libraries.ActivityPub.Helpers;

public static class WebfingerHelper
{
    public static Webfinger BuildWebfinger(
        string domainName,
        Guid userId,
        string username)
    {
        var webfinger = new Webfinger
        {
            Subject = $"acct:{username}@{domainName}",
            Links = new List<Link>
            {
                new()
                {
                    Rel = "self",
                    Href = UriHelper.GetId(domainName, userId),
                    Type = "application/activity+json"
                }
            }
        };

        return webfinger;
    }
}