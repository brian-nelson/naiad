using LiteDB;
using Naiad.Libraries.Core.Interfaces;

namespace Naiad.Libraries.System.LiteDb.Providers;

public class LiteDbRepositoryProvider : IRepositoryProvider
{
    public LiteDbRepositoryProvider(
        string filename)
    {
        var mapper = BsonMapper.Global;
    }
}

