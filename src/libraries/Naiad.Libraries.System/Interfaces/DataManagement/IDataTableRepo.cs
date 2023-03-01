using System.Collections.Generic;
using System.Data;

namespace Naiad.Libraries.System.Interfaces.DataManagement;

public interface IDataTableRepo
{
    public int GetCount();

    public DataTable GetAllData();

    public DataTable GetData(int skip, int limit);

    public List<string> GetColumns();

    public void SaveData(DataTable table);

    // TODO - Add query capability
}