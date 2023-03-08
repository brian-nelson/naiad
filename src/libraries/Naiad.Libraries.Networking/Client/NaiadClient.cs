using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Naiad.Libraries.Networking.Objects;
using Naiad.Libraries.Core.Constants;
using Naiad.Libraries.Core.Helpers;

namespace Naiad.Libraries.Networking.Client;

public class NaiadClient
{
    private readonly string _url;
    private readonly string _accessKey;
    private readonly string _secretKey;
    private HttpClient _client;
    private string _jwt;

    public NaiadClient(
        string url,
        string accessKey,
        string secretKey)
    {
        _url = url;
        _accessKey = accessKey;
        _secretKey = secretKey;
    }

    private string BuildUrl(string partialUrl)
    {
        return _url + partialUrl;
    }

    public void Login()
    {
        var url = BuildUrl("api/login/authtoken");

        _client = new HttpClient();

        var accessKey = new AccessKeyRequest
        {
            Key = _accessKey,
            SecretKey = _secretKey
        };

        string inputJson = JsonHelper.ToJson(accessKey);
        var data = new StringContent(inputJson, Encoding.UTF8, MimeTypeConstants.JSON);
        var responseTask = _client.PostAsync(url, data);
        var response = responseTask.Result;

        var outputTask = response.Content.ReadAsStringAsync();
        var outputJson = outputTask.Result;

        var jwtResponse = JsonHelper.ToObject<LoginResponse>(outputJson);
        _jwt = jwtResponse.JWT;

        _client.DefaultRequestHeaders.Add("AUTHORIZATION", $"BEARER {_jwt}");
    }

    public void Logout()
    {
        var url = BuildUrl("api/logout");
        var responseTask = _client.PostAsync(url, null);
        var response = responseTask.Result;

        _client.DefaultRequestHeaders.Remove("AUTHORIZATION");
    }

    public Guid UploadData(
        string filePath,
        string fileName,
        Stream fileStream)
    {
        using (var form = new MultipartFormDataContent())
        {
            var streamContent = new StreamContent(fileStream);

            form.Add(streamContent, "file", fileName);

            var url = BuildUrl($"api/data/{filePath}{fileName}");

            var responseTask = _client.PostAsync(url, form);
            var response = responseTask.Result;

            var responseContentTask = response.Content.ReadAsStringAsync();
            var responseJson = responseContentTask.Result;

            var uploadDataResponse = JsonHelper.ToObject<UploadDataResponse>(responseJson);
            return uploadDataResponse.DataPointerId;
        }
    }

    public Stream DownloadData(
        string filePathAndName)
    {
        string url = BuildUrl($"api/data/{filePathAndName}");
        var responseTask = _client.GetAsync(url);
        var response = responseTask.Result;

        var stream = response.Content.ReadAsStream();
        return stream;
    }
}