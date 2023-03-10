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

        public bool WasSuccessful { get; set; }

        public string Result { get; set; }
    }
}
