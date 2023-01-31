using Naiad.Libraries.System.Models.MetadataManagement;
using System;
using System.Collections.Generic;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface ICategorizationRepo
{
    public IEnumerable<Categorization> GetAll();
    public void Save(Categorization zone);
    public Categorization GetById(Guid id);
}

