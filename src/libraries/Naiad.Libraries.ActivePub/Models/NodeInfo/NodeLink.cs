using System;

namespace Naiad.Libraries.ActivityPub.Models.NodeInfo;

public class NodeLink
{
    public string Rel { get; set; }
    public Uri Href { get; set; }
}