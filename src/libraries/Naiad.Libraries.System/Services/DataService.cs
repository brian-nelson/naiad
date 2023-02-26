using System.Collections.Generic;
using System.IO;
using System.Linq;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.Services;

public class DataService
{
    private readonly IStorageRepo _storageProvider;
    private readonly MetadataService _metadataService;

    public DataService(
        IRepositoryProvider repositoryProvider,
        MetadataService metadataService)
    {
        _storageProvider = repositoryProvider.GetStorageProvider();
        _metadataService = metadataService;
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
        return _storageProvider.FileExists(fileId);
    }

    public NaiadFileInfo GetFileInfo(string fileId)
    {
        var file = _storageProvider.GetFileInfo(fileId);
        if (file == null)
        {
            return null;
        }

        var pointer = _metadataService.GetDataPointer(fileId);
        if (pointer != null)
        {
            file.DataPointerId = pointer.Id;
        }

        return file;
    }

    public IEnumerable<NaiadFileInfo> ListFiles(string prefix)
    {
        var files = _storageProvider.ListFiles(prefix).ToList();

        foreach (var file in files)
        {
            var pointer = _metadataService.GetDataPointer(file.Id);
            if (pointer != null)
            {
                file.DataPointerId = pointer.Id;
            }
        }

        return files;
    }

    public void DeleteFile(string fileId)
    {
        _storageProvider.DeleteFile(fileId);
    }
}
