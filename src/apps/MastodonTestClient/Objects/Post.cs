using System;

namespace MastodonTestClient.Objects
{
    public class Post
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string Content { get; set; }
    }
}
