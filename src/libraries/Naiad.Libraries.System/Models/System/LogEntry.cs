using System;

namespace Naiad.Libraries.System.Models.System
{
    public class LogEntry : AbstractDbRecord
    {
        public DateTimeOffset EntryDateTime { get; set; }

        public string Level { get; set; }

        public string Message { get; set; }

        public Guid? UserId { get; set; }
    }
}
