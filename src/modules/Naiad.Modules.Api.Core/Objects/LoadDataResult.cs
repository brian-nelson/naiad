using System;

namespace Naiad.Modules.Api.Core.Objects;

public class LoadDataResult
{
    public Guid DataPointerId { get; set; }
    public string FileId { get; set; }
}