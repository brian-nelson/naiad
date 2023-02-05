using LiteDB;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.LiteDb.Providers;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Naiad.Libraries.System.LiteDb.Tests.Repos.MetadataManagement;

internal class GranularityRepoTests
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
        var repo = _provider.GetGranularityRepo();

        var dp = new Granularity
        {
            Id = Guid.NewGuid(),
            Name = RandomHelper.GetRandomAlphaString(10)
        };

        repo.Save(dp);
    }

    [Test]
    public void TestGetById()
    {
        var repo = _provider.GetGranularityRepo();

        var dp = new Granularity
        {
            Id = Guid.NewGuid(),
            Name = RandomHelper.GetRandomAlphaString(10)
        };

        repo.Save(dp);

        var test = repo.GetById(dp.Id);
        Assert.IsNotNull(test);
        Assert.AreEqual(dp.Id, test.Id);
        Assert.AreEqual(dp.Name, test.Name);
    }

    [Test]
    public void TestGetAll()
    {
        var repo = _provider.GetGranularityRepo();

        var dp = new Granularity
        {
            Id = Guid.NewGuid(),
            Name = RandomHelper.GetRandomAlphaString(10)
        };

        repo.Save(dp);

        var test = repo.GetAll();
        Assert.GreaterOrEqual(test.Count(), 1);
    }
}

