using System;

namespace Naiad.Libraries.System.Models.System
{
    public class SystemEvent : AbstractDbRecord
    {
        public SystemEvent()
        {
            Time = DateTime.UtcNow;
        }

        public DateTime Time { get; set; }

        public string Type { get; set; }

        public Guid ObjectId { get; set; }

        public Guid PerformedByUserId { get; set; }
    }
}
