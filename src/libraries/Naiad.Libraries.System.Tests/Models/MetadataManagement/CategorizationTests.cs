using System;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.MetadataManagement;

[TestFixture]
public class CategorizationTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var name = RandomHelper.GetRandomAlphaNumericString(10);

        var c = new Categorization
        {
            Id = id,
            Name = name
        };

        Assert.AreEqual(id, c.Id);
        Assert.AreEqual(name, c.Name);
    }
}

