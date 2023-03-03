using System;

namespace Naiad.Libraries.ActivityPub.Models.General;

public class Link
{
    public string Rel { get; set; }
    public string Type { get; set; }
    public Uri Href { get; set; }
}
