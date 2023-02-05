using System.Collections.Generic;
using System.IO;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.Services;

public class DataService
{
    private readonly IStorageProvider _storageProvider;

    public DataService(
        IStorageProvider storageProvider)
    {
        _storageProvider = storageProvider;
    }

    public void SaveFile(string fileId, Stream stream)
    {
        _storageProvider.SaveFile(fileId, stream);
    }

    public NaiadFileInfo GetFileInfo(string fileId)
    {
        return _storageProvider.GetFileInfo(fileId);
    }

    public IEnumerable<NaiadFileInfo> ListFiles(string prefix)
    {
        return _storageProvider.ListFiles(prefix);
    }
}
