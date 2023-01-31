using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface IRelationshipRepo
{
    public Relationship GetById(Guid id);
    public IEnumerable<Relationship> GetChildren(Guid parentId);
    public IEnumerable<Relationship> GetParents(Guid childId);
    public void Save(Relationship relationship);
}