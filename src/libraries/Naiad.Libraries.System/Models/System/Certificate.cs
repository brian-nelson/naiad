using System;
using Naiad.Libraries.System.Constants.System;

namespace Naiad.Libraries.System.Models.System
{
    public class Certificate : AbstractDbRecord
    {
        public CertificateTypes CertificateType { get; set; }

        public string CertificateContents { get; set; }

        public DateTimeOffset ValidFrom { get; set; }

        public DateTimeOffset ValidTo { get; set; }

        public bool IsValid { get; set; }
    }
}
