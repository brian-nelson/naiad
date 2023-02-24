using System.Data;

namespace Naiad.Libraries.System.Interfaces.DataManagement;

public interface IDataTableProvider
{
    public DataTable GetAllData();

    public void SaveData(DataTable table);

    // TODO - Add query capability
}