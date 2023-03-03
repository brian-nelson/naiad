using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IConfigurationRepo : IDataRepository<Configuration>
{
    public Configuration GetByKey(string key);
}