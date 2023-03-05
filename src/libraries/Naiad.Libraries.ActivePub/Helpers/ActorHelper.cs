using Naiad.Libraries.ActivityPub.Models.Pub;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.ActivityPub.Helpers
{
    public static class ActorHelper
    {
        public static string GetUsername(
            string resource,
            string domainName)
        {
            var prefix = "acct:";
            var suffix = $"@{domainName}";

            if (resource != null)
            {
                if (resource.StartsWith(prefix)
                    && resource.EndsWith(suffix))
                {
                    string output = resource.Substring(prefix.Length);
                    output = output.Substring(0, output.Length - suffix.Length);

                    return output;
                }
            }

            return null;
        }

        public static Actor CreateActor(
            User user,
            string domainName)
        {
            Actor actor = new()
            {
                //Summary = actorDto.Summary,
                PreferredUsername = user.Username,
                Name = user.FullName,
                //Type = user.Type,
                Id = UriHelper.GetId(domainName, user.Id),
                Inbox = UriHelper.GetInbox(domainName, user.Id),
                Outbox = UriHelper.GetOutbox(domainName, user.Id),
                Following = UriHelper.GetFollowing(domainName, user.Id),
                Followers = UriHelper.GetFollowers(domainName, user.Id),
                Context = new[]
                {
                    "https://www.w3.org/ns/activitystreams",
                    "https://w3id.org/security/v1"
                }
            };

            return actor;
        }
    }
}
