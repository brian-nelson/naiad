using System;

namespace Naiad.Libraries.System.Models.System;

public class KnownInstance : AbstractDbRecord
{
    public string Name { get; set; }

    public string HostName { get; set; }

    public Guid PublicKeyId { get; set; }

    /// <summary>
    /// Only set for localhost
    /// </summary>
    public Guid? PrivateKeyId { get; set; }
}