using System;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.MetadataManagement;

[TestFixture]
public class MetadataPropertyTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var key = RandomHelper.GetRandomAlphaNumericString(10);
        var value = RandomHelper.GetRandomAlphaNumericString(50);

        var mdp = new MetadataProperty
        {
            Id = id,
            Key = key,
            Value = value
        };

        Assert.AreEqual(id, mdp.Id);
        Assert.AreEqual(key, mdp.Key);
        Assert.AreEqual(value, mdp.Value);
    }
}

