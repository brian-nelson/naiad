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
public class MetadataPropertyRepoTests
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
        var repo = _provider.GetMetadataPropertyRepo();

        var dp = new MetadataProperty
        {
            Id = Guid.NewGuid(),
            MetadataId = Guid.NewGuid(),
            Key = RandomHelper.GetRandomAlphaString(10),
            Value = RandomHelper.GetRandomAlphaString(10)
        };

        repo.Save(dp);
    }

    [Test]
    public void TestGetById()
    {
        var repo = _provider.GetMetadataPropertyRepo();

        var dp = new MetadataProperty
        {
            Id = Guid.NewGuid(),
            MetadataId = Guid.NewGuid(),
            Key = RandomHelper.GetRandomAlphaString(10),
            Value = RandomHelper.GetRandomAlphaString(10)
        };

        repo.Save(dp);

        var test = repo.GetById(dp.Id);
        Assert.IsNotNull(test);
        Assert.AreEqual(dp.Id, test.Id);
        Assert.AreEqual(dp.MetadataId, test.MetadataId);
        Assert.AreEqual(dp.Key, test.Key);
        Assert.AreEqual(dp.Value, test.Value);
    }

    [Test]
    public void TestGetByMetadata()
    {
        var repo = _provider.GetMetadataPropertyRepo();

        var dp = new MetadataProperty
        {
            Id = Guid.NewGuid(),
            MetadataId = Guid.NewGuid(),
            Key = RandomHelper.GetRandomAlphaString(10),
            Value = RandomHelper.GetRandomAlphaString(10)
        };

        repo.Save(dp);

        var testRecords = repo
            .Get(dp.MetadataId)
            .ToList();
        MetadataProperty test = testRecords[0];

        Assert.IsNotNull(test);
        Assert.AreEqual(dp.Id, test.Id);
        Assert.AreEqual(dp.MetadataId, test.MetadataId);
        Assert.AreEqual(dp.Key, test.Key);
        Assert.AreEqual(dp.Value, test.Value);
    }
}
