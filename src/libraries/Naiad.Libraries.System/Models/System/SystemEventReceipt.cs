using System;

namespace Naiad.Libraries.System.Models.System
{
    public class SystemEventReceipt : AbstractDbRecord
    {
        public SystemEventReceipt()
        {
            Time = DateTime.UtcNow;
        }

        public Guid EventId { get; set; }

        public DateTime Time { get; set; }

        public Guid HandlerConfigurationId { get; set; }

        public string Result { get; set; }

        public string Details { get; set; }
    }
}
