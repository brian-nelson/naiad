using LiteDB;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Interfaces;

namespace Naiad.Libraries.System.LiteDb.Providers;

public class LiteDbRepositoryProvider : IRepositoryProvider
{
    public LiteDbRepositoryProvider(
        ILiteDatabase database)
    {
        Database = database;

        var mapper = BsonMapper.Global;

        // Metadata
        mapper.Entity<IDbRecord>()
            .Id(x => x.Id);
    }

    public ILiteDatabase Database { get; }
}

