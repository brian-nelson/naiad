using LiteDB;
using Naiad.Libraries.System.Interfaces.DataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.DataManagement
{
    public class CollectionProvider : ICollectionProvider
    {
        private readonly ILiteDatabase _database;

        public CollectionProvider(ILiteDatabase database)
        {
            _database = database;
        }

        public void SaveJson(string dataType, string json)
        {
            var collection = _database.GetCollection(dataType);

            var document = LiteDB.JsonSerializer.Deserialize(json);
            
        }
    }
}
