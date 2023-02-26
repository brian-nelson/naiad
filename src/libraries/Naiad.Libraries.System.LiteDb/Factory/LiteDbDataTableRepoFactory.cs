using LiteDB;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.LiteDb.Repos.DataManagement;
using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.LiteDb.Factory
{
    public class LiteDbDataTableRepoFactory : IDataTableRepoFactory
    {
        private readonly ILiteDatabase _liteDatabase;

        public LiteDbDataTableRepoFactory(
            ILiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public IDataTableRepo GetDataTableRepo(StructuredDataDefinition sdd)
        {
            var dataTableRepo = new LiteDbDataTableRepo(
                _liteDatabase, sdd);

            return dataTableRepo;
        }
    }
}
