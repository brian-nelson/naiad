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
public class AccessKeyRepoTests
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
        var repo = _provider.GetAccessKeyRepo();

        var ak = new AccessKey()
        {
            CreatedDateTime = RandomHelper.GetRandomDateTimeOffset(10),
            HashedSecret = RandomHelper.GetRandomAlphaString(10),
            IsEnabled = true,
            Key = RandomHelper.GetRandomAlphaString(10),
            Salt = RandomHelper.GetRandomAlphaString(10),
            UserId = Guid.NewGuid()
        };

        repo.Save(ak);
    }

    [Test]
    public void TestGetById()
    {
        var repo = _provider.GetAccessKeyRepo();

        var ak = new AccessKey()
        {
            CreatedDateTime = RandomHelper.GetRandomDateTimeOffset(10),
            HashedSecret = RandomHelper.GetRandomAlphaString(10),
            IsEnabled = true,
            Key = RandomHelper.GetRandomAlphaString(10),
            Salt = RandomHelper.GetRandomAlphaString(10),
            UserId = Guid.NewGuid()
        };

        repo.Save(ak);

        var test = repo.GetById(ak.Id);
        Assert.NotNull(test);

        Assert.AreEqual(ak.Id, test.Id);
        AssertionHelper.ApproximatelyEqual(ak.CreatedDateTime, test.CreatedDateTime);
        Assert.AreEqual(ak.HashedSecret, test.HashedSecret);
        Assert.AreEqual(ak.IsEnabled, test.IsEnabled);
        Assert.AreEqual(ak.Key, test.Key);
        Assert.AreEqual(ak.Salt, test.Salt);
        Assert.AreEqual(ak.UserId, test.UserId);
    }

    [Test]
    public void TestGetByUserId()
    {
        var repo = _provider.GetAccessKeyRepo();

        var ak = new AccessKey()
        {
            CreatedDateTime = RandomHelper.GetRandomDateTimeOffset(10),
            HashedSecret = RandomHelper.GetRandomAlphaString(10),
            IsEnabled = true,
            Key = RandomHelper.GetRandomAlphaString(10),
            Salt = RandomHelper.GetRandomAlphaString(10),
            UserId = Guid.NewGuid()
        };

        repo.Save(ak);

        var testRecords = repo
            .GetByUserId(ak.UserId)
            .ToList();
        Assert.GreaterOrEqual(1, testRecords.Count);

        var test = testRecords[0];

        Assert.AreEqual(ak.Id, test.Id);
        AssertionHelper.ApproximatelyEqual(ak.CreatedDateTime, test.CreatedDateTime); 
        Assert.AreEqual(ak.HashedSecret, test.HashedSecret);
        Assert.AreEqual(ak.IsEnabled, test.IsEnabled);
        Assert.AreEqual(ak.Key, test.Key);
        Assert.AreEqual(ak.Salt, test.Salt);
        Assert.AreEqual(ak.UserId, test.UserId);
    }
}

