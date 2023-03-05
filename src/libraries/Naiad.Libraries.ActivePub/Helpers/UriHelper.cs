using Naiad.Libraries.ActivityPub.Models.Environment;
using System;

namespace Naiad.Libraries.ActivityPub.Helpers
{
    public static class UriHelper
    {
        public static Uri GetId(
            string domainName,
            Guid userId)
        {
            return new Uri($"https://{domainName}/actor/{userId}");
        }

        public static Uri GetInbox(
            string domainName,
            Guid userId)
        {
            return new Uri($"https://{domainName}/inbox/{userId}");
        }

        public static Uri GetOutbox(
            string domainName,
            Guid userId)
        {
            return new Uri($"https://{domainName}/outbox/{userId}");
        }

        public static Uri GetFollowing(
            string domainName,
            Guid userId)
        {
            return new Uri($"https://{domainName}/following/{userId}");
        }

        public static Uri GetFollowers(
            string domainName,
            Guid userId)
        {
            return new Uri($"https://{domainName}/followers/{userId}");
        }

        public static Uri GetNodeInfo(
            string domainName)
        {
            return new Uri($"https://{domainName}/nodeinfo/2.0");
        }
    }
}
