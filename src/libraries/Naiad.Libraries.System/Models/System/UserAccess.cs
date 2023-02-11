using System;

namespace Naiad.Libraries.System.Models.System;

public class UserAccess 
    : AbstractDbRecord
{
    public Guid UserId { get; set; }

    public string HashedPassword { get; set; }

    public string Salt { get; set; }

    public DateTimeOffset SetOnDateTime { get; set; }
}