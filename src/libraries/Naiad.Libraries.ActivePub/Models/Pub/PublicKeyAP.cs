using System;

namespace Naiad.Libraries.ActivePub.Models.Pub;

public class PublicKeyAP
{
    public Uri Id { get; set; }
    public Uri Owner { get; set; }
    public string PublicKeyPem { get; set; }
}