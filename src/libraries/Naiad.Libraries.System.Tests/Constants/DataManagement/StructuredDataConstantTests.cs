using Naiad.Libraries.System.Constants.DataManagement;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Constants.DataManagement
{
    [TestFixture]
    public class StructuredDataConstantTests
    {
        [Test]
        public void TestConstants()
        {
            Assert.AreEqual("NAIAD_STRUCTURED_DATA", StructuredDataConstants.NAIAD_STRUCTURED_DATA);
            Assert.AreEqual("NSD_NAME", StructuredDataConstants.NSD_NAME);
            Assert.AreEqual("NSD_MIME_TYPE", StructuredDataConstants.NSD_MIME_TYPE);
            Assert.AreEqual("NSD_IDENTIFIER_NAME", StructuredDataConstants.NSD_IDENTIFIER_NAME);
            Assert.AreEqual("NSD_COLLECTION_NAME", StructuredDataConstants.NSD_COLLECTION_NAME);
        }
    }
}
