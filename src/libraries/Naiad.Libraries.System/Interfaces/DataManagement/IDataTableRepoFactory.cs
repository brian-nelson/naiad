using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.Interfaces.DataManagement;

public interface IDataTableRepoFactory
{
    public IDataTableProvider GetDataTableRepo(StructuredDataDefinition sdd);
}