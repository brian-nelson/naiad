using System;

namespace Naiad.Libraries.Networking.Objects;

public class UploadDataResponse
{
    public Guid DataPointerId { get; set; }
    public string FileId { get; set; }
}