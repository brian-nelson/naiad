using System;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.MetadataManagement;

[TestFixture]
public class DataPointerTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var zoneId = Guid.NewGuid();
        var granularityId = Guid.NewGuid();
        var storageLocation = RandomHelper.GetRandomFilename(4);

        var dp = new DataPointer
        {
            Id = id,
            ZoneId = zoneId,
            GranularityId = granularityId,
            StorageLocation = storageLocation
        };

        Assert.AreEqual(id, dp.Id);
        Assert.AreEqual(zoneId, dp.ZoneId);
        Assert.AreEqual(granularityId, dp.GranularityId);
        Assert.AreEqual(storageLocation, dp.StorageLocation);
    }

    [Test]
    public void TestGetSetWithNulls()
    {
        var id = Guid.NewGuid();
        var storageLocation = RandomHelper.GetRandomFilename(4);

        var dp = new DataPointer
        {
            Id = id,
            ZoneId = null,
            GranularityId = null,
            StorageLocation = storageLocation
        };

        Assert.AreEqual(id, dp.Id);
        Assert.IsNull(dp.ZoneId);
        Assert.IsNull(dp.GranularityId);
        Assert.AreEqual(storageLocation, dp.StorageLocation);
    }
}

