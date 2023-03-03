using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Services;
using Naiad.Modules.Api.Core.Helpers;
using Naiad.Modules.Api.Core.Objects;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Web.Helpers;

namespace Naiad.Modules.Api.Core.Controllers;

[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly SystemService _systemService;
    private readonly string _jwtSecret;
    private readonly INaiadLogger _logger;

    public AuthController(
        SystemService systemService,
        JwtSecret jwtSecret,
        INaiadLogger logger)
    {
        _systemService = systemService;
        _jwtSecret = jwtSecret.Value;
        _logger = logger;
    }

    [HttpPost]
    [Route("api/login")]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest loginRequest)
    {
        var webSession = _systemService.CreateSession(loginRequest.Email, loginRequest.Password, out User user);

        if (webSession != null)
        {
            var jwt = JwtHelper.CreateJwt(webSession, _jwtSecret);
            var response = new LoginResponse
            {
                JWT = jwt
            };

            _logger.Info("User logged in", user.Id);

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

        _logger.Info("User logged out", User.GetUserId());


        return Ok();
    }
}
