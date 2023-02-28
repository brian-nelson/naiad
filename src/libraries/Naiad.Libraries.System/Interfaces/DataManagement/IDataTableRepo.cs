using System.Data;

namespace Naiad.Libraries.System.Interfaces.DataManagement;

public interface IDataTableRepo
{
    public DataTable GetAllData();

    public DataTable GetData(int skip, int limit);

    public void SaveData(DataTable table);

    // TODO - Add query capability
}