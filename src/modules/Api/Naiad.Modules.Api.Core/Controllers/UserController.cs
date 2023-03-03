using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Services;
using System.Collections.Generic;
using System;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Models.System;
using Naiad.Modules.Api.Core.Helpers;
using Naiad.Modules.Api.Core.Objects;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.Web.Helpers;

namespace Naiad.Modules.Api.Core.Controllers;

[Authorize]
[Produces("application/json")]
public class UserController 
    : ControllerBase
{
    private readonly SystemService _systemService;
    private readonly INaiadLogger _logger;

    public UserController(
        SystemService systemService,
        INaiadLogger logger)
    {
        _systemService = systemService;
        _logger = logger;
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    [Route("api/users")]
    public ActionResult<IEnumerable<UserDto>> GetUsers()
    {
        var users = _systemService.GetUsers();
        _logger.Info($"All users retrieved", User.GetUserId());
        return Ok(users.ToUserDtos());
    }

    [HttpGet]
    [Route("api/user/{userId:guid}")]
    public ActionResult<User> GetUser(
        Guid userId)
    {
        var user = _systemService.GetUser(userId);
        _logger.Info($"User retrieved ({userId})", User.GetUserId());
        return Ok(user.ToUserDto());
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("api/user")]
    public ActionResult SaveUser(
        [FromBody] UserDto userDto)
    {
        var user = userDto.ToUser();
        _systemService.Save(user);

        _logger.Info($"User saved ({user.Id})", User.GetUserId());
        
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
        
        _logger.Info($"Password set ({userId})", User.GetUserId());

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

        _logger.Info($"Password changed", User.GetUserId());

        return Ok();
    }
}
