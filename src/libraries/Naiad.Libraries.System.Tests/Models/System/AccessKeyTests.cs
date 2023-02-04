using System;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.System;

[TestFixture]
public class AccessKeyTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var key = RandomHelper.GetRandomAlphaNumericString(10);
        var createdDateTime = RandomHelper.GetRandomDateTimeOffset(30);
        var hashedSecret = RandomHelper.GetRandomAlphaNumericString(50);
        var salt = RandomHelper.GetRandomAlphaNumericString(10);
        var userId = Guid.NewGuid();

        var ak = new AccessKey
        {
            Id = id,
            Key = key,
            CreatedDateTime = createdDateTime,
            HashedSecret = hashedSecret,
            Salt = salt,
            UserId = userId
        };

        Assert.AreEqual(id, ak.Id);
        Assert.AreEqual(key, ak.Key);
        Assert.AreEqual(hashedSecret, ak.HashedSecret);
        Assert.IsTrue(ak.IsEnabled);
        Assert.AreEqual(salt, ak.Salt);
        Assert.AreEqual(userId, ak.UserId);
        
        ak.IsEnabled = false;
        Assert.IsFalse(ak.IsEnabled);

    }
}
