using System.Collections.Generic;

namespace Naiad.Libraries.ActivePub.Models.General;

public class Webfinger
{
    public string Subject { get; set; }
    public IEnumerable<Link> Links { get; set; }
}