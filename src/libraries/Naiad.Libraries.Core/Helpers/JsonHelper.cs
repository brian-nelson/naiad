using Newtonsoft.Json;

namespace Naiad.Libraries.Core.Helpers
{
    public static class JsonHelper
    {
        public static string ToJson<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public static T ToObject<T>(string json)
        {
            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}
