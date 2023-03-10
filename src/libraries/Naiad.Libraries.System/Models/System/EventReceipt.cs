using System;

namespace Naiad.Libraries.System.Models.System
{
    public class EventReceipt : AbstractDbRecord
    {
        public EventReceipt()
        {
            Time = DateTime.UtcNow;
        }

        public Guid EventId { get; set; }

        public DateTime Time { get; set; }

        public string HandlerType { get; set; }

        public Guid HandlerConfigurationId { get; set; }
    }
}
