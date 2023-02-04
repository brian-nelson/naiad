using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;
using System;

namespace Naiad.Libraries.System.Tests.Models.MetadataManagement;

public class GranularityTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var name = RandomHelper.GetRandomAlphaNumericString(10);

        var g = new Granularity()
        {
            Id = id,
            Name = name
        };

        Assert.AreEqual(id, g.Id);
        Assert.AreEqual(name, g.Name);
    }
}

