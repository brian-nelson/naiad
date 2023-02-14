using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Services;
using System.Collections.Generic;
using System;
using Naiad.Libraries.System.Models.System;
using Naiad.Modules.Api.Core.Helpers;
using Naiad.Modules.Api.Core.Objects;

namespace Naiad.Modules.Api.Core.Controllers;

[Authorize]
[Produces("application/json")]
public class UserController 
    : ControllerBase
{
    private readonly SystemService _systemService;

    public UserController(
        SystemService systemService)
    {
        _systemService = systemService;
    }

    [HttpGet]
    [Route("api/user")]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = _systemService.GetUsers();
        return Ok(users);
    }

    [HttpGet]
    [Route("api/user/{userId:guid}")]
    public ActionResult<User> GetUser(
        Guid userId)
    {
        var user = _systemService.GetUser(userId);

        return Ok(user);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("api/user")]
    public ActionResult SaveUser(
        [FromBody] User user)
    {
        _systemService.Save(user);

        return Ok();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("api/user/{userId:guid}/password")]
    public ActionResult SetPassword(
        [FromRoute] Guid userId,
        [FromBody] SetPasswordRequest request)
    {
        _systemService.SetPassword(
            userId, 
            request.NewPassword);

        return Ok();
    }

    [HttpPost]
    [Route("api/user/password")]
    public ActionResult ChangePassword(
        [FromBody] ChangePasswordRequest request)
    {
        _systemService.ChangePassword(
            User.GetUserId(),
            request.OldPassword,
            request.NewPassword);

        return Ok();
    }

}
