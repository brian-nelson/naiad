using System;
using Naiad.Libraries.System.Constants.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.MetadataManagement;

[TestFixture]
public class RelationshipTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var childId = Guid.NewGuid();
        var childType = EntityType.DataPointer;
        var connectionContext = RandomHelper.GetRandomAlphaNumericString(10);
        var parentId = Guid.NewGuid();
        var parentType = EntityType.Metadata;

        var r = new Relationship
        {
            Id = id,
            ChildId = childId,
            ChildType = childType,
            ConnectionContext = connectionContext,
            ParentId = parentId,
            ParentType = parentType
        };

        Assert.AreEqual(id, r.Id);
        Assert.AreEqual(childId, r.ChildId);
        Assert.AreEqual(childType, r.ChildType);
        Assert.AreEqual(connectionContext, r.ConnectionContext);
        Assert.AreEqual(parentId, r.ParentId);
        Assert.AreEqual(parentType, r.ParentType);
    }

    [Test]
    public void TestGetSetWithNulls()
    {
        var id = Guid.NewGuid();
        var childId = Guid.NewGuid();
        var childType = EntityType.DataPointer;
        var parentId = Guid.NewGuid();
        var parentType = EntityType.Metadata;

        var r = new Relationship
        {
            Id = id,
            ChildId = childId,
            ChildType = childType,
            ConnectionContext = null,
            ParentId = parentId,
            ParentType = parentType
        };

        Assert.AreEqual(id, r.Id);
        Assert.AreEqual(childId, r.ChildId);
        Assert.AreEqual(childType, r.ChildType);
        Assert.IsNull(r.ConnectionContext);
        Assert.AreEqual(parentId, r.ParentId);
        Assert.AreEqual(parentType, r.ParentType);
    }
}
