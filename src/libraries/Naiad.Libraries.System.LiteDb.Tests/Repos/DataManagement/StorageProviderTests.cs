using System;
using System.IO;
using System.Linq;
using LiteDB;
using Naiad.Libraries.Core.Helpers;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.LiteDb.Providers;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.LiteDb.Tests.Repos.DataManagement;

[TestFixture]
public class StorageProviderTests
{
    private ILiteDatabase _db;
    private IRepositoryProvider _provider;
    private string _dbName;
    private const string WorkingFolder = "\\Temp";
    private string _testFile;

    [OneTimeSetUp]
    public void Setup()
    {
        DirectoryHelper.EnsureDirectory(WorkingFolder);

        _dbName = Path.Combine(WorkingFolder, RandomHelper.GetRandomAlphaString(10) + ".ldb");
        _db = new LiteDatabase(_dbName);
        _provider = new LiteDbRepositoryProvider(_db);

        _testFile = Path.Combine(Environment.CurrentDirectory,
            "Resources",
            "random_binary.bin");
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        //TODO - delete file
    }

    [Test]
    public void TestSaveFile()
    {
        var sp = _provider.GetStorageProvider();

        string fileId = RandomHelper.GetRandomFilename(4);

        using (var fs = File.OpenRead(_testFile))
        {
            sp.SaveFile(fileId, fs);
        }
    }

    [Test]
    public void TestGetFile()
    {
        var sp = _provider.GetStorageProvider();

        string fileId = RandomHelper.GetRandomFilename(4);
        using (var fs = File.OpenRead(_testFile))
        {
            sp.SaveFile(fileId, fs);
        }


        string sourceHash;
        using (var fs = File.OpenRead(_testFile))
        {
            sourceHash = HashHelper.ComputeHash(fs);
        }

        string testHash;
        using (var stream = sp.GetFile(fileId))
        {
            testHash = HashHelper.ComputeHash(stream);
        }

        Assert.AreEqual(sourceHash, testHash);
    }

    [Test]
    public void TestGetFileInfo()
    {
        var sp = _provider.GetStorageProvider();

        var fileInfo = new FileInfo(_testFile);

        string fileId = RandomHelper.GetRandomFilename(4);
        using (var fs = File.OpenRead(_testFile))
        {
            sp.SaveFile(fileId, fs);
        }

        var naiadFileInfo = sp.GetFileInfo(fileId);
        Assert.AreEqual(fileInfo.Length, naiadFileInfo.Size);
        Assert.AreEqual(fileId, naiadFileInfo.Id);
        Assert.AreEqual(Path.GetFileName(fileId), naiadFileInfo.Filename);
        Assert.AreEqual("application/octet-stream", naiadFileInfo.MimeType);
    }

    [Test]
    public void TestListFiles()
    {
        var sp = _provider.GetStorageProvider();

        string fileId = RandomHelper.GetRandomFilename(4);
        using (var fs = File.OpenRead(_testFile))
        {
            sp.SaveFile(fileId, fs);
        }

        var files = sp.ListFiles();

        Assert.GreaterOrEqual(files.Count(), 1);
    }

    [Test]
    public void DeleteFile()
    {
        var sp = _provider.GetStorageProvider();

        string fileId = RandomHelper.GetRandomFilename(4);
        using (var fs = File.OpenRead(_testFile))
        {
            sp.SaveFile(fileId, fs);
        }

        var fileInfo = sp.GetFileInfo(fileId);
        Assert.IsNotNull(fileInfo);

        sp.DeleteFile(fileId);

        fileInfo = sp.GetFileInfo(fileId);
        Assert.IsNull(fileInfo);
    }
}
