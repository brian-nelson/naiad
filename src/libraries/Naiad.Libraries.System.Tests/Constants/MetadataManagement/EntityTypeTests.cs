using Naiad.Libraries.System.Constants.MetadataManagement;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Constants.MetadataManagement;

[TestFixture]
public class EntityTypeTests
{
    [Test]
    public void TestEnumValues()
    {
        Assert.AreEqual(0, (int)EntityType.Metadata);
        Assert.AreEqual(1, (int)EntityType.DataPointer);
    }
}

