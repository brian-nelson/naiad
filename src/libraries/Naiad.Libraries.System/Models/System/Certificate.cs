using System;
using Naiad.Libraries.System.Constants.System;

namespace Naiad.Libraries.System.Models.System
{
    public class Certificate : AbstractDbRecord
    {
        public CertificateType CertificateType { get; set; }

        public string CertificateContents { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public bool IsValid { get; set; }
    }
}
