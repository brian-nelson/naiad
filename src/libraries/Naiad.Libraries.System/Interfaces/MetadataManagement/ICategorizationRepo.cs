﻿using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.Interfaces.MetadataManagement;

public interface ICategorizationRepo : IDataRepository<Categorization>
{
    public Categorization GetByName(string name);
}

