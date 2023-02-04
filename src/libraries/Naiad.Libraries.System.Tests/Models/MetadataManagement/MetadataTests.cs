using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naiad.Libraries.System.Models.MetadataManagement;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.MetadataManagement;

[TestFixture]
internal class MetadataTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var categorizationId = Guid.NewGuid();
        
        var md = new Metadata
        {
            Id = id,
            CategorizationId = categorizationId
        };

        Assert.AreEqual(id, md.Id);
        Assert.AreEqual(categorizationId, md.CategorizationId);
    }

    [Test]
    public void TestGetSetWithNulls()
    {
        var id = Guid.NewGuid();
        
        var md = new Metadata
        {
            Id = id,
            CategorizationId = null
        };

        Assert.AreEqual(id, md.Id);
        Assert.IsNull(md.CategorizationId);
    }
}

