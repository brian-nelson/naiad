using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.LiteDb.Helpers;
using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.DataManagement
{
    public class FileProvider : IStorageProvider
    {
        private readonly ILiteStorage<string> _fileStorage;

        public FileProvider(ILiteDatabase database)
        {
            _fileStorage = database.FileStorage;
        }

        public Stream GetFile(string fileId)
        {
            var fileInfo = _fileStorage.FindById(fileId);

            var blah = fileInfo.OpenRead();
            return blah;

        }

        public void SaveFile(string fileId, Stream stream)
        {
            _fileStorage.Upload(fileId, fileId, stream);
        }

        public NaiadFileInfo GetFileInfo(string fileId)
        {
            var lfi = _fileStorage.FindById(fileId);

            if (lfi != null)
            {
                return lfi.ToNaiadFileInfo();
            }

            return null;
        }

        public IEnumerable<NaiadFileInfo> ListFiles(string prefix)
        {
            var output = new List<NaiadFileInfo>();

            var files = _fileStorage.Find(prefix);

            foreach (LiteFileInfo<string> lfi in files)
            {
                NaiadFileInfo nfi = lfi.ToNaiadFileInfo();

                output.Add(nfi);
            }

            return output;

        }

        public void DeleteFile(string fileId)
        {
            _fileStorage.Delete(fileId);
        }
    }
}
