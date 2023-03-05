using Naiad.Libraries.ActivityPub.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.ActivityPub.Tests.Helpers;


[TestFixture]
public class ActorHelperTests
{
    [Test]
    public void TestGetUsername()
    {
        string username = "myusername";
        string domain = "mydomain.com";
        string resource = $"acct:{username}@{domain}";

        string result = ActorHelper.GetUsername(resource, domain);

        Assert.AreEqual(username, result);
    }
}