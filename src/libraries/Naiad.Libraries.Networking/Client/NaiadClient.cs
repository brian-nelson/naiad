using Naiad.Libraries.Networking.Helpers;
using Naiad.Libraries.Networking.Objects;

namespace Naiad.Libraries.Networking.Client;

public class NaiadClient
{
    private readonly string _url;
    private readonly string _accessKey;
    private readonly string _secretKey;
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

    public void Login()
    {
        var accessKey = new AccessKeyRequest
        {
            Key = _accessKey,
            SecretKey = _secretKey
        };

        var response = HttpHelper.Post<LoginResponse, AccessKeyRequest>(_url + "api/login/authtoken", accessKey);
        _jwt = response.JWT;
    }




}