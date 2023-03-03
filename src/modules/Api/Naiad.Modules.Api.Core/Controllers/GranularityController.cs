using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.System.Services;
using System.Collections.Generic;
using System;
using Naiad.Libraries.System.Interfaces;
using Naiad.Modules.Api.Core.Helpers;

namespace Naiad.Modules.Api.Core.Controllers;

[Authorize]
[Produces("application/json")]
public class GranularityController : ControllerBase
{
    private readonly MetadataService _metadataService;
    private readonly INaiadLogger _logger;

    public GranularityController(
        MetadataService metadataService,
        INaiadLogger logger)
    {
        _metadataService = metadataService;
        _logger = logger;
    }

    [HttpGet]
    [Route("api/granularities")]
    public ActionResult<IEnumerable<Granularity>> GetGranularities()
    {
        var granularities = _metadataService.GetGranularities();
        _logger.Info($"All granularities retrieved", User.GetUserId());
        return Ok(granularities);
    }

    [HttpGet]
    [Route("api/granularity/{granularityId:guid}")]
    public ActionResult<Categorization> GetGranularity(Guid granularityId)
    {
        var granularity = _metadataService.GetGranularity(granularityId);
        _logger.Info($"Granularity retrieved ({granularityId})", User.GetUserId());
        return Ok(granularity);
    }

    [Authorize(Roles = "Administrator,ReadWrite")]
    [HttpPost]
    [Route("api/granularity")]
    public ActionResult SaveGranularity([FromBody] Granularity granularity)
    {
        _metadataService.Save(granularity);
        _logger.Info($"Granularity saved ({granularity.Id})", User.GetUserId());
        return Ok();
    }
}