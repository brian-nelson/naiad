using System;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.System;

[TestFixture]
public class KnownInstanceTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var hostName = RandomHelper.GetRandomAlphaNumericString(20);
        var lastTimeSeen = RandomHelper.GetRandomDateTimeOffset(20);
        var name = RandomHelper.GetRandomAlphaNumericString(50);
        var privateKey = Guid.NewGuid();
        var publicKeyId = Guid.NewGuid();

        var ki = new KnownInstance
        {
            Id = id,
            HostName = hostName,
            LastTimeSeen = lastTimeSeen,
            Name = name,
            PrivateKeyId = privateKey,
            PublicKeyId = publicKeyId
        };

        Assert.AreEqual(id, ki.Id);
        Assert.AreEqual(hostName, ki.HostName);
        Assert.AreEqual(lastTimeSeen, ki.LastTimeSeen);
        Assert.AreEqual(name, ki.Name);
        Assert.AreEqual(privateKey, ki.PrivateKeyId);
        Assert.AreEqual(publicKeyId, ki.PublicKeyId);

        ki.PrivateKeyId = null;
        Assert.IsNull(ki.PrivateKeyId);

    }
}

