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
public class MetadataRepoTests
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
        var repo = _provider.GetMetadataRepo();

        var md = new Metadata
        {
            Id = Guid.NewGuid(),
            CategorizationId = Guid.NewGuid()
        };

        repo.Save(md);
    }

    [Test]
    public void TestGetById()
    {
        var repo = _provider.GetMetadataRepo();

        var md = new Metadata
        {
            Id = Guid.NewGuid(),
            CategorizationId = Guid.NewGuid()
        };

        repo.Save(md);

        var test = repo.GetById(md.Id);

        Assert.IsNotNull(test);
        Assert.AreEqual(md.Id, test.Id);
        Assert.AreEqual(md.CategorizationId, test.CategorizationId);
    }

    [Test]
    public void TestGetByCategorization()
    {
        var repo = _provider.GetMetadataRepo();

        var md = new Metadata
        {
            Id = Guid.NewGuid(),
            CategorizationId = Guid.NewGuid()
        };

        repo.Save(md);

        var testRecords = repo
            .Get(md.CategorizationId.Value)
            .ToList();
        Assert.AreEqual(1, testRecords.Count);

        var test = testRecords[0];
        Assert.IsNotNull(test);
        Assert.AreEqual(md.Id, test.Id);
        Assert.AreEqual(md.CategorizationId, test.CategorizationId);
    }
}
