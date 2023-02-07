using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IConfigurationRepo
{
    public Configuration GetById(Guid configurationId);
    public IEnumerable<Configuration> GetAll();
    public void Save(Configuration configuration);
    public Configuration GetByKey(string key);
}