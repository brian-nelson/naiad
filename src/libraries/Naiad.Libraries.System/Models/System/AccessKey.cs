using System;

namespace Naiad.Libraries.System.Models.System
{
    public class AccessKey : AbstractDbRecord
    {
        public Guid UserId { get; set; }

        public string Key { get; set; }

        public string HashedSecret { get; set; }

        public string Salt { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public bool IsEnabled { get; set; }
    }
}
