using System;
using System.IO;
using System.Linq;
using LiteDB;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.LiteDb.Providers;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;
using AssertionHelper = Naiad.Libraries.Testing.Helpers.AssertionHelper;

namespace Naiad.Libraries.System.LiteDb.Tests.Repos.System;

[TestFixture]
public class LogEntryRepoTests
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
        var repo = _provider.GetLogEntryRepo();

        var le = new LogEntry
        {
            UserId = Guid.NewGuid(),
            EntryDateTime = RandomHelper.GetRandomDateTimeOffset(30),
            Level = RandomHelper.GetRandomAlphaString(10),
            Message = RandomHelper.GetRandomAlphaString(100)
        };

        repo.Save(le);
    }

    [Test]
    public void TestGetById()
    {
        var repo = _provider.GetLogEntryRepo();

        var le = new LogEntry
        {
            UserId = Guid.NewGuid(),
            EntryDateTime = RandomHelper.GetRandomDateTimeOffset(30),
            Level = RandomHelper.GetRandomAlphaString(10),
            Message = RandomHelper.GetRandomAlphaString(100)
        };

        repo.Save(le);

        var test = repo.GetById(le.Id);
        Assert.NotNull(test);

        Assert.AreEqual(le.Id, test.Id);
        AssertionHelper.ApproximatelyEqual(le.EntryDateTime, test.EntryDateTime);
        Assert.AreEqual(le.Level, test.Level);
        Assert.AreEqual(le.Message, test.Message);
    }

    [Test]
    public void TestGetByUserId()
    {
        var repo = _provider.GetLogEntryRepo();

        var le = new LogEntry
        {
            UserId = Guid.NewGuid(),
            EntryDateTime = RandomHelper.GetRandomDateTimeOffset(30),
            Level = RandomHelper.GetRandomAlphaString(10),
            Message = RandomHelper.GetRandomAlphaString(100)
        };

        repo.Save(le);

        var testRecords = repo.GetByUser(
                le.UserId.Value,
                le.EntryDateTime.DateTime.AddDays(-5),
                le.EntryDateTime.DateTime.AddDays(5))
            .ToList();
        Assert.AreEqual(1, testRecords.Count);

        var test = testRecords[0];
        Assert.AreEqual(le.Id, test.Id);
        AssertionHelper.ApproximatelyEqual(le.EntryDateTime, test.EntryDateTime);
        Assert.AreEqual(le.Level, test.Level);
        Assert.AreEqual(le.Message, test.Message);
    }

    [Test]
    public void TestGet()
    {
        var repo = _provider.GetLogEntryRepo();

        var le = new LogEntry
        {
            UserId = Guid.NewGuid(),
            EntryDateTime = RandomHelper.GetRandomDateTimeOffset(30),
            Level = RandomHelper.GetRandomAlphaString(10),
            Message = RandomHelper.GetRandomAlphaString(100)
        };

        repo.Save(le);

        var testRecords = repo.Get(
                le.EntryDateTime.DateTime.AddDays(-5),
                le.EntryDateTime.DateTime.AddDays(5))
            .ToList();
        Assert.GreaterOrEqual(1, testRecords.Count);

        var test = testRecords[0];
        Assert.AreEqual(le.Id, test.Id);
        AssertionHelper.ApproximatelyEqual(le.EntryDateTime, test.EntryDateTime);
        Assert.AreEqual(le.Level, test.Level);
        Assert.AreEqual(le.Message, test.Message);
    }
}
