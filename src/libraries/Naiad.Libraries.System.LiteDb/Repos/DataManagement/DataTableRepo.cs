using System.Data;
using LiteDB;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.Models.DataManagement;
using NotImplementedException = System.NotImplementedException;

namespace Naiad.Libraries.System.LiteDb.Repos.DataManagement
{
    public class DataTableRepo : IDataTableProvider
    {
        private readonly ILiteDatabase _database;
        private readonly StructuredDataDefinition _structuredDataDefinition;

        public DataTableRepo(
            ILiteDatabase database,
            StructuredDataDefinition structuredDataDefinition)
        {
            _database = database;
            _structuredDataDefinition = structuredDataDefinition;
        }
        
        public DataTable GetAllData()
        {
            throw new NotImplementedException();
        }

        public void SaveData(DataTable table)
        {
            throw new NotImplementedException();
        }
    }
}
