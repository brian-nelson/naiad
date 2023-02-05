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
public class CategorizationRepoTests
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
        var repo = _provider.GetCategorizationRepo();

        var categorization = new Categorization
        {
            Name = RandomHelper.GetRandomAlphaString(10)
        };

        repo.Save(categorization);
    }

    [Test]
    public void TestGetById()
    {
        var repo = _provider.GetCategorizationRepo();

        var categorization = new Categorization
        {
            Name = RandomHelper.GetRandomAlphaString(10)
        };

        repo.Save(categorization);

        var test = repo.GetById(categorization.Id);
        Assert.NotNull(test);
        Assert.AreEqual(categorization.Id, test.Id);
        Assert.AreEqual(categorization.Name, test.Name);
    }

    [Test]
    public void TestGetAll()
    {
        var repo = _provider.GetCategorizationRepo();

        var categorization = new Categorization
        {
            Name = RandomHelper.GetRandomAlphaString(10)
        };

        repo.Save(categorization);

        var all = repo.GetAll();
        Assert.GreaterOrEqual(all.Count(), 1);
    }
}
