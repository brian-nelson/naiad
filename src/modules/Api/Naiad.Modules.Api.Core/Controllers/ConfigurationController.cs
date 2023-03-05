using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Services;
using Naiad.Libraries.Web.Helpers;
using System.Collections.Generic;
using Naiad.Libraries.System.Models.System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Naiad.Modules.Api.Core.Controllers;

public class ConfigurationController : ControllerBase
{
    private readonly SystemService _systemService;
    private readonly INaiadLogger _logger;

    public ConfigurationController(
        SystemService systemService,
        INaiadLogger logger)
    {
        _systemService = systemService;
        _logger = logger;
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    [Route("api/configurations")]
    public ActionResult<IEnumerable<Configuration>> GetConfigurations()
    {
        var configurations = _systemService.GetExternalConfigurations();

        _logger.Info("All configurations retrieved", User.GetUserId());

        return Ok(configurations);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    [Route("api/configuration/{key}")]
    public ActionResult<Configuration> GetConfiguration(
        [FromRoute] string key)
    {
        var configuration = _systemService.GetConfiguration(key);

        if (configuration != null
            && configuration.IsInternal)
        {
            _logger.Info($"Attempt to retrieve internal configuration ({key})", User.GetUserId());
            throw new UnauthorizedAccessException("Internal configuration is not externally accessible");
        }

        _logger.Info($"Configuration retrieved ({key})", User.GetUserId());
        return Ok(configuration);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("api/configuration")]
    public ActionResult SaveConfiguration([FromBody] Configuration configuration)
    {
        if (configuration.IsInternal)
        {
            _logger.Info($"Attempt to save internal configuration ({configuration.Key})", User.GetUserId());
            throw new UnauthorizedAccessException("Internal configuration is not externally accessible");
        }

        _systemService.Save(configuration);

        _logger.Info($"Configuration saved ({configuration.Id})", User.GetUserId());
        return Ok();
    }
}

