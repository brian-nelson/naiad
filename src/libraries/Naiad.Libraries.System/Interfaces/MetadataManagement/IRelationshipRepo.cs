using System;
using System.Collections.Generic;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface IRelationshipRepo : IDataRepository<Relationship>
{
    public IEnumerable<Relationship> GetChildren(Guid parentId);
    public IEnumerable<Relationship> GetParents(Guid childId);

    public Relationship GetRelationship(
        Guid parentId,
        Guid childId,
        string connectionContext);
}