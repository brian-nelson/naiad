using System;
using Naiad.Libraries.System.Interfaces.System;

namespace Naiad.Libraries.System.Models.System
{
    public class AccessKey : IDbRecord
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Key { get; set; }

        public string HashedSecret { get; set; }
    }
}
