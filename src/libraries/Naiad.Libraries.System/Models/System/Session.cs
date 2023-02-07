using System;

namespace Naiad.Libraries.System.Models.System;

public class Session : AbstractDbRecord
{
    public Guid UserId { get; set; }

    public DateTimeOffset CreatedOnDateTime { get; set; }

    public DateTimeOffset ExpiresOnDateTime { get; set; }

    public bool IsDeleted { get; set; }
}