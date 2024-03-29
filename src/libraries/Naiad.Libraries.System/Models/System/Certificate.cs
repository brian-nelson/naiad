﻿using System;
using Naiad.Libraries.System.Constants.System;

namespace Naiad.Libraries.System.Models.System;

public class Certificate 
    : AbstractDbRecord
{
    public Guid CertificateOwnerId { get; set; }

    public CertificateTypes CertificateType { get; set; }

    public string PublicKeyPem { get; set; }

    public string PrivateKeyPem { get; set; }

    public DateTimeOffset ValidFrom { get; set; }

    public DateTimeOffset ValidTo { get; set; }

    public bool IsValid { get; set; }
}