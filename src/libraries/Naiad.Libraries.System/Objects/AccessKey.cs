using System;
using Naiad.Libraries.System.Interfaces;

namespace Naiad.Libraries.System.Objects
{
    public class AccessKey : IDbRecord
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Key { get; set; }

        public string HashedSecret { get; set; }
    }
}
