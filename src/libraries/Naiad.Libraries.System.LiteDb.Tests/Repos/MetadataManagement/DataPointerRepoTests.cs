using System;
using System.IO;
using System.Linq;
using LiteDB;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.LiteDb.Providers;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.LiteDb.Tests.Repos.MetadataManagement;

[TestFixture]
public class DataPointerRepoTests
{
    private ILiteDatabase _db;
    private IRepositoryProvider _provider;
    private string _dbName;
    private const string WorkingFolder = "\\Temp";

    [OneTimeSetUp]
    public void Setup()
    {
        _dbName = Path.Combine(WorkingFolder, RandomHelper.GetRandomAlphaString(10) + ".ldb");
        _db = new LiteDatabase(_dbName);
        _provider = new LiteDbRepositoryProvider(_db);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        //TODO - delete file
    }

    [Test]
    public void TestSave()
    {
        var repo = _provider.GetDataPointerRepo();

        var dp = new DataPointer
        {
            GranularityId = Guid.NewGuid(),
            ZoneId = Guid.NewGuid(),
            StorageLocation = RandomHelper.GetRandomFilename(4)
        };

        repo.Save(dp);
    }

    [Test]
    public void TestGetById()
    {
        var repo = _provider.GetDataPointerRepo();

        var dp = new DataPointer
        {
            GranularityId = Guid.NewGuid(),
            ZoneId = Guid.NewGuid(),
            StorageLocation = RandomHelper.GetRandomFilename(4)
        };

        repo.Save(dp);

        var test = repo.GetById(dp.Id);
        Assert.NotNull(test);
        Assert.AreEqual(dp.Id, test.Id);
        Assert.AreEqual(dp.GranularityId, test.GranularityId);
        Assert.AreEqual(dp.ZoneId, test.ZoneId);
        Assert.AreEqual(dp.StorageLocation, test.StorageLocation);
    }

    [Test]
    public void TestGetByZone()
    {
        var repo = _provider.GetDataPointerRepo();

        var dp = new DataPointer
        {
            GranularityId = Guid.NewGuid(),
            ZoneId = Guid.NewGuid(),
            StorageLocation = RandomHelper.GetRandomFilename(4)
        };

        repo.Save(dp);

        var all = repo.GetByZone(dp.ZoneId.Value);
        Assert.AreEqual(1, all.Count());
    }

    [Test]
    public void TestGetByGranularity()
    {
        var repo = _provider.GetDataPointerRepo();

        var dp = new DataPointer
        {
            GranularityId = Guid.NewGuid(),
            ZoneId = Guid.NewGuid(),
            StorageLocation = RandomHelper.GetRandomFilename(4)
        };

        repo.Save(dp);

        var all = repo.GetByGranularity(dp.GranularityId.Value);
        Assert.AreEqual(1, all.Count());
    }

    [Test]
    public void TestByZoneAndGranularity()
    {
        var repo = _provider.GetDataPointerRepo();

        var dp = new DataPointer
        {
            GranularityId = Guid.NewGuid(),
            ZoneId = Guid.NewGuid(),
            StorageLocation = RandomHelper.GetRandomFilename(4)
        };

        repo.Save(dp);

        var all = repo.GetByZoneAndGranularity(
            dp.ZoneId.Value,
            dp.GranularityId.Value);
        Assert.AreEqual(1, all.Count());
    }
}

