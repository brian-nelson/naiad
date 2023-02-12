using System.Collections.Generic;
using System.IO;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.Services;

public class DataService
{
    private readonly IStorageProvider _storageProvider;

    public DataService(
        IRepositoryProvider repositoryProvider)
    {
        _storageProvider = repositoryProvider.GetStorageProvider();
    }

    public void SaveFile(string fileId, Stream stream)
    {
        _storageProvider.SaveFile(fileId, stream);
    }

    public Stream GetFile(string fileId)
    {
        return _storageProvider.GetFile(fileId);
    }

    public bool FileExists(string fileId)
    {
        return _storageProvider.FileExist(fileId);
    }

    public NaiadFileInfo GetFileInfo(string fileId)
    {
        return _storageProvider.GetFileInfo(fileId);
    }

    public IEnumerable<NaiadFileInfo> ListFiles(string prefix)
    {
        return _storageProvider.ListFiles(prefix);
    }

    public void DeleteFile(string fileId)
    {
        _storageProvider.DeleteFile(fileId);
    }
}
