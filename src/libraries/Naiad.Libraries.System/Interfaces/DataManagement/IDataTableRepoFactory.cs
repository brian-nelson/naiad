using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.Interfaces.DataManagement;

public interface IDataTableRepoFactory
{
    public IDataTableRepo GetDataTableRepo(StructuredDataDefinition sdd);
}