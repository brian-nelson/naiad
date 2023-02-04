using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naiad.Libraries.System.Models.DataManagement;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.DataManagement
{
    [TestFixture]
    public class FileInfoTests
    {
        [Test]
        public void TestGetSet()
        {
            var id = RandomHelper.GetRandomAlphaNumericString(20);
            var filename = RandomHelper.GetRandomAlphaNumericString(20);
            var mimetype = RandomHelper.GetRandomAlphaNumericString(20);
            var size = RandomHelper.GetRandomInt(1, 1000);

            var nfi = new NaiadFileInfo
            {
                Id = id,
                Filename = filename,
                MimeType = mimetype,
                Size = size
            };

            Assert.AreEqual(id, nfi.Id);
            Assert.AreEqual(filename, nfi.Filename);
            Assert.AreEqual(mimetype, nfi.MimeType);
            Assert.AreEqual(size, nfi.Size);
        }
    }
}
