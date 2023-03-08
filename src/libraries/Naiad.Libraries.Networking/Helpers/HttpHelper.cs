using System.Net.Http;
using System.Text;
using Naiad.Libraries.Core.Constants;
using Naiad.Libraries.Core.Helpers;

namespace Naiad.Libraries.Networking.Helpers
{
    public static class HttpHelper
    {
        public static T Post<T, U>(string url, U obj)
        {
            HttpClient client = new HttpClient();

            string inputJson = JsonHelper.ToJson(obj);
            var data = new StringContent(inputJson, Encoding.UTF8, MimeTypeConstants.JSON);
            var responseTask = client.PostAsync(url, data);
            var response = responseTask.Result;

            var outputTask = response.Content.ReadAsStringAsync();
            var outputJson = outputTask.Result;

            return JsonHelper.ToObject<T>(outputJson);
        }

        public static void Post(string url)
        {

            
        }
    }
}
