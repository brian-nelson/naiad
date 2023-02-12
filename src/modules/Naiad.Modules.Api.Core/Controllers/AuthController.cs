using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.System.Services;
using Naiad.Modules.Api.Core.Helpers;
using Naiad.Modules.Api.Core.Objects;
using System;

namespace Naiad.Modules.Api.Core.Controllers;

[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly SystemService _systemService;
    private readonly string _jwtSecret;

    public AuthController(
        SystemService systemService,
        JwtSecret jwtSecret)
    {
        _systemService = systemService;
        _jwtSecret = jwtSecret.Value;
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

    [Authorize]
    [HttpPost]
    [Route("api/logout")]
    public ActionResult Logout()
    {
        var sessionId = User.GetSessionId();
        _systemService.CloseSession(sessionId);

        return Ok();
    }
}
