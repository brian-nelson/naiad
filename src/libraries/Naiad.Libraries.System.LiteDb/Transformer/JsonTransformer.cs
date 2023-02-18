using LiteDB;
using Naiad.Libraries.System.Services;

namespace Naiad.Libraries.System.LiteDb.Transformer;

public class JsonTransformer
{
    private readonly SystemService _systemService;
    private readonly MetadataService _metadataService;

    public JsonTransformer(
        SystemService systemService,
        MetadataService metadataService)
    {
        _systemService = systemService;
        _metadataService = metadataService;
    }

    public BsonDocument Convert(string json)
    {
        return null;
    }
}
