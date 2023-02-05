using System;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.System;

[TestFixture]
public class UserTests
{
    [Test]
    public void TestGetSet()
    {
        Guid id = Guid.NewGuid();
        string email = RandomHelper.GetRandomAlphaNumericString(20).ToLower();
        string familyName = RandomHelper.GetRandomAlphaNumericString(10);
        string givenName = RandomHelper.GetRandomAlphaNumericString(10);

        var u = new User
        {
            Id = id,
            IsEnabled = true,
            Email = email,
            FamilyName = familyName,
            GivenName = givenName,
            MustChangePassword = true
        };

        Assert.AreEqual(id, u.Id);
        Assert.IsTrue(u.IsEnabled);
        Assert.AreEqual(email, u.Email);
        Assert.AreEqual(familyName, u.FamilyName);
        Assert.AreEqual(givenName, u.GivenName);
        Assert.IsTrue(u.MustChangePassword);

        u.IsEnabled = false;
        Assert.IsFalse(u.IsEnabled);

        u.MustChangePassword = false;
        Assert.IsFalse(u.MustChangePassword);
    }
}

