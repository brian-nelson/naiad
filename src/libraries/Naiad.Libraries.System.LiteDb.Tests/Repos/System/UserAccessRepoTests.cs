using System;
using System.IO;
using LiteDB;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.LiteDb.Providers;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;
using AssertionHelper = Naiad.Libraries.Testing.Helpers.AssertionHelper;

namespace Naiad.Libraries.System.LiteDb.Tests.Repos.System;

[TestFixture]
public class UserAccessRepoTests
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
        var repo = _provider.GetUserAccessRepo();

        var ua = new UserAccess
        {
            HashedPassword = RandomHelper.GetRandomAlphaString(10),
            Salt = RandomHelper.GetRandomAlphaString(10),
            SetOnDateTime = RandomHelper.GetRandomDateTimeOffset(30),
            UserId = Guid.NewGuid()
        };

        repo.Save(ua);
    }

    [Test]
    public void TestGetById()
    {
        var repo = _provider.GetUserAccessRepo();

        var ua = new UserAccess
        {
            HashedPassword = RandomHelper.GetRandomAlphaString(10),
            Salt = RandomHelper.GetRandomAlphaString(10),
            SetOnDateTime = RandomHelper.GetRandomDateTimeOffset(30),
            UserId = Guid.NewGuid()
        };

        repo.Save(ua);

        var test = repo.GetById(ua.Id);
        Assert.NotNull(test);

        Assert.AreEqual(ua.Id, test.Id);
        AssertionHelper.ApproximatelyEqual(ua.SetOnDateTime, test.SetOnDateTime);
        Assert.AreEqual(ua.HashedPassword, test.HashedPassword);
        Assert.AreEqual(ua.Salt, test.Salt);
        Assert.AreEqual(ua.UserId, test.UserId);
    }

    [Test]
    public void TestGetByUserId()
    {
        var repo = _provider.GetUserAccessRepo();

        var ua = new UserAccess
        {
            HashedPassword = RandomHelper.GetRandomAlphaString(10),
            Salt = RandomHelper.GetRandomAlphaString(10),
            SetOnDateTime = RandomHelper.GetRandomDateTimeOffset(30),
            UserId = Guid.NewGuid()
        };

        repo.Save(ua);

        var test = repo.GetByUserId(ua.UserId);
        Assert.NotNull(test);

        Assert.AreEqual(ua.Id, test.Id);
        AssertionHelper.ApproximatelyEqual(ua.SetOnDateTime, test.SetOnDateTime);
        Assert.AreEqual(ua.HashedPassword, test.HashedPassword);
        Assert.AreEqual(ua.Salt, test.Salt);
        Assert.AreEqual(ua.UserId, test.UserId);
    }
}
