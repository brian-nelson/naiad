using System;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.System;

[TestFixture]
public class UserAccessTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var salt = RandomHelper.GetRandomAlphaNumericString(50);
        var setOnDateTime = RandomHelper.GetRandomDateTimeOffset(30);
        var hashedPassword = RandomHelper.GetRandomAlphaNumericString(50);

        var ua = new UserAccess
        {
            Id = id,
            UserId = userId,
            Salt = salt,
            SetOnDateTime = setOnDateTime,
            HashedPassword = hashedPassword
        };

        Assert.AreEqual(id, ua.Id);
        Assert.AreEqual(userId, ua.UserId);
        Assert.AreEqual(salt, ua.Salt);
        Assert.AreEqual(setOnDateTime, ua.SetOnDateTime);
        Assert.AreEqual(hashedPassword, ua.HashedPassword);
    }
}
