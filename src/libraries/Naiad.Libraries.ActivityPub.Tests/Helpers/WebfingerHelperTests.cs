using System;
using System.Linq;
using Naiad.Libraries.ActivityPub.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.ActivityPub.Tests.Helpers;

[TestFixture]
public class WebfingerHelperTests
{

    [Test]
    public void TestCreateFromActor()
    {
        Guid userId = Guid.NewGuid();
        string domain = "mydomain.com";
        string username = "myusername";
        string uriUrl = $"https://{domain}/actor/{userId}";
        string subject = $"acct:{username}@{domain}";


        var webfinger = WebfingerHelper.BuildWebfinger(
            domain,
            userId,
            username);

        Assert.IsNotNull(webfinger);
        Assert.AreEqual(subject, webfinger.Subject);
        Assert.AreNotEqual(Guid.Empty, webfinger.Id);

        var link = webfinger.Links.FirstOrDefault();
        Assert.IsNotNull(link);
        Assert.AreEqual("self", link.Rel);
        Assert.AreEqual(uriUrl, link.Href.OriginalString);
        Assert.AreEqual("application/activity+json", link.Type);
    }
}
