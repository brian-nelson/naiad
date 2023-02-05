using System;
using System.IO;
using LiteDB;
using Naiad.Libraries.System.Constants.System;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.LiteDb.Providers;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.LiteDb.Tests.Repos.System;

[TestFixture]
public class UserRepoTests
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
        var repo = _provider.GetUserRepo();

        var ua = new User
        {
            Email = RandomHelper.GetRandomAlphaString(10),
            FamilyName = RandomHelper.GetRandomAlphaString(10),
            GivenName = RandomHelper.GetRandomAlphaString(10),
            IsEnabled = true,
            MustChangePassword = false,
            UserType = UserTypes.Administrator
        };

        repo.Save(ua);
    }

    [Test]
    public void TestGetById()
    {
        var repo = _provider.GetUserRepo();

        var u = new User
        {
            Email = RandomHelper.GetRandomAlphaString(10),
            FamilyName = RandomHelper.GetRandomAlphaString(10),
            GivenName = RandomHelper.GetRandomAlphaString(10),
            IsEnabled = true,
            MustChangePassword = false,
            UserType = UserTypes.Administrator
        };

        repo.Save(u);

        var test = repo.GetById(u.Id);
        Assert.NotNull(test);

        Assert.AreEqual(u.Id, test.Id);
        Assert.AreEqual(u.FamilyName, test.FamilyName);
        Assert.AreEqual(u.GivenName, test.GivenName);
        Assert.AreEqual(u.IsEnabled, test.IsEnabled);
        Assert.AreEqual(u.MustChangePassword, test.MustChangePassword);
        Assert.AreEqual(u.UserType, test.UserType);
    }

    [Test]
    public void TestGetByEmail()
    {
        var repo = _provider.GetUserRepo();

        var u = new User
        {
            Email = RandomHelper.GetRandomAlphaString(10),
            FamilyName = RandomHelper.GetRandomAlphaString(10),
            GivenName = RandomHelper.GetRandomAlphaString(10),
            IsEnabled = true,
            MustChangePassword = false,
            UserType = UserTypes.Administrator
        };

        repo.Save(u);

        var test = repo.GetByEmail(u.Email);
        Assert.NotNull(test);

        Assert.AreEqual(u.Id, test.Id);
        Assert.AreEqual(u.FamilyName, test.FamilyName);
        Assert.AreEqual(u.GivenName, test.GivenName);
        Assert.AreEqual(u.IsEnabled, test.IsEnabled);
        Assert.AreEqual(u.MustChangePassword, test.MustChangePassword);
        Assert.AreEqual(u.UserType, test.UserType);
    }
}
