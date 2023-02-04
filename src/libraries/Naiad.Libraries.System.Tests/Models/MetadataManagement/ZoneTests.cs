using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.MetadataManagement;

[TestFixture]
public class ZoneTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var name = RandomHelper.GetRandomAlphaNumericString(10);

        var z = new Zone()
        {
            Id = id,
            Name = name
        };

        Assert.AreEqual(id, z.Id);
        Assert.AreEqual(name, z.Name);
    }
}

