using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.System.Services;
using Naiad.Modules.Api.Core.Helpers;
using Naiad.Modules.Api.Core.Objects;
using System;

namespace Naiad.Modules.Api.Core.Controllers;

[Authorize]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly SystemService _systemService;
    private readonly string _jwtSecret;

    public AuthController(
        SystemService systemService)
    {
        _systemService = systemService;

        var configuration = _systemService.GetConfiguration("JWT_SECRET");
        if (configuration == null)
        {
            configuration = new Configuration
            {
                Key = "JWT_SECRET",
                Value = JwtHelper.CreateJwtSecret()
            };

            _systemService.Save(configuration);
        }

        _jwtSecret = configuration.Value;
    }

    [HttpPost]
    [Route("api/login")]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest loginRequest)
    {
        var webSession = _systemService.CreateSession(loginRequest.Email, loginRequest.Password);

        if (webSession != null)
        {
            var jwt = JwtHelper.CreateJwt(webSession, _jwtSecret);
            var response = new LoginResponse
            {
                JWT = jwt
            };

            return Ok(response);
        }

        return Unauthorized();
    }

    [HttpPost]
    [Route("api/logout/{sessionId:Guid}")]
    public ActionResult Logout([FromRoute] Guid sessionId)
    {
        _systemService.CloseSession(sessionId);
        return Ok();
    }
}
