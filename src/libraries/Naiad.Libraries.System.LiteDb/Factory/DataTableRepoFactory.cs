using LiteDB;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.LiteDb.Repos.DataManagement;
using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.LiteDb.Factory
{
    public class DataTableRepoFactory : IDataTableRepoFactory
    {
        private readonly ILiteDatabase _liteDatabase;

        public DataTableRepoFactory(
            ILiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public IDataTableProvider GetDataTableRepo(StructuredDataDefinition sdd)
        {
            var dataTableRepo = new DataTableRepo(
                _liteDatabase, sdd);

            return dataTableRepo;
        }
    }
}
