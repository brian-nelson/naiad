﻿using Naiad.Libraries.System.Models.DataManagement;
using System.Collections.Generic;
using System.IO;

namespace Naiad.Libraries.System.Interfaces.DataManagement;

public interface IStorageRepo
{
    public Stream GetFile(string fileId);

    public bool FileExists(string fileId);

    public void SaveFile(string fileId, Stream stream);

    public NaiadFileInfo GetFileInfo(string fileId);

    public IEnumerable<NaiadFileInfo> ListFiles(string prefix = null);

    public void DeleteFile(string fileId);
}