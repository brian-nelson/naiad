using System;
using System.IO;
using LiteDB;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.LiteDb.Providers;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.LiteDb.Tests.Repos.MetadataManagement;

[TestFixture]
public class ZoneRepoTests
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
        var repo = _provider.GetZonRepo();

        var z = new Zone
        {
            Id = Guid.NewGuid(),
            Name = RandomHelper.GetRandomAlphaString(10)
        };

        repo.Save(z);
    }

    [Test]
    public void TestGetById()
    {
        var repo = _provider.GetZonRepo();

        var z = new Zone
        {
            Id = Guid.NewGuid(),
            Name = RandomHelper.GetRandomAlphaString(10)
        };

        repo.Save(z);

        var test = repo.GetById(z.Id);

        Assert.IsNotNull(test);
        Assert.AreEqual(z.Id, test.Id);
        Assert.AreEqual(z.Name, test.Name);
    }
}
