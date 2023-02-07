using Naiad.Libraries.System.Constants.System;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Constants.System;

[TestFixture]
public class UserTypesTests
{
    [Test]
    public void TestUserTypesValues()
    {
        Assert.AreEqual(0, (int)UserTypes.ReadOnly);
        Assert.AreEqual(1, (int)UserTypes.ReadWrite);
        Assert.AreEqual(2, (int)UserTypes.Administrator);
    }
}
