using System;
using System.IO;
using System.Linq;
using LiteDB;
using Naiad.Libraries.System.Constants.MetadataManagement;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.LiteDb.Providers;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.LiteDb.Tests.Repos.MetadataManagement;

[TestFixture]
public class RelationshipRepoTests
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
        var repo = _provider.GetRelationshipRepo();

        var md = new Relationship
        {
            Id = Guid.NewGuid(),
            ChildId = Guid.NewGuid(),
            ChildType = EntityType.DataPointer,
            ConnectionContext = RandomHelper.GetRandomAlphaString(10),
            ParentId = Guid.NewGuid(),
            ParentType = EntityType.Metadata
        };

        repo.Save(md);
    }

    [Test]
    public void TestGetById()
    {
        var repo = _provider.GetRelationshipRepo();

        var r = new Relationship
        {
            Id = Guid.NewGuid(),
            ChildId = Guid.NewGuid(),
            ChildType = EntityType.DataPointer,
            ConnectionContext = RandomHelper.GetRandomAlphaString(10),
            ParentId = Guid.NewGuid(),
            ParentType = EntityType.Metadata
        };

        repo.Save(r);

        var test = repo.GetById(r.Id);

        Assert.IsNotNull(test);
        Assert.AreEqual(r.Id, test.Id);
        Assert.AreEqual(r.ChildId, test.ChildId);
        Assert.AreEqual(r.ChildType, test.ChildType);
        Assert.AreEqual(r.ConnectionContext, test.ConnectionContext);
        Assert.AreEqual(r.ParentId, test.ParentId);
        Assert.AreEqual(r.ParentType, test.ParentType);
    }

    [Test]
    public void TestGetChildren()
    {
        var repo = _provider.GetRelationshipRepo();

        var r = new Relationship
        {
            Id = Guid.NewGuid(),
            ChildId = Guid.NewGuid(),
            ChildType = EntityType.DataPointer,
            ConnectionContext = RandomHelper.GetRandomAlphaString(10),
            ParentId = Guid.NewGuid(),
            ParentType = EntityType.Metadata
        };

        repo.Save(r);

        var testRecords = repo
            .GetChildren(r.ParentId)
            .ToList();
        Assert.AreEqual(1, testRecords.Count);

        var test = testRecords[0];
        Assert.IsNotNull(test);
        Assert.AreEqual(r.Id, test.Id);
        Assert.AreEqual(r.ChildId, test.ChildId);
        Assert.AreEqual(r.ChildType, test.ChildType);
        Assert.AreEqual(r.ConnectionContext, test.ConnectionContext);
        Assert.AreEqual(r.ParentId, test.ParentId);
        Assert.AreEqual(r.ParentType, test.ParentType);
    }

    [Test]
    public void TestGetParents()
    {
        var repo = _provider.GetRelationshipRepo();

        var r = new Relationship
        {
            Id = Guid.NewGuid(),
            ChildId = Guid.NewGuid(),
            ChildType = EntityType.DataPointer,
            ConnectionContext = RandomHelper.GetRandomAlphaString(10),
            ParentId = Guid.NewGuid(),
            ParentType = EntityType.Metadata
        };

        repo.Save(r);

        var testRecords = repo
            .GetParents(r.ChildId)
            .ToList();
        Assert.AreEqual(1, testRecords.Count);

        var test = testRecords[0];
        Assert.IsNotNull(test);
        Assert.AreEqual(r.Id, test.Id);
        Assert.AreEqual(r.ChildId, test.ChildId);
        Assert.AreEqual(r.ChildType, test.ChildType);
        Assert.AreEqual(r.ConnectionContext, test.ConnectionContext);
        Assert.AreEqual(r.ParentId, test.ParentId);
        Assert.AreEqual(r.ParentType, test.ParentType);
    }
}
