using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.System.Services;
using System.Collections.Generic;
using System;

namespace Naiad.Modules.Api.Core.Controllers;

[Authorize]
[Produces("application/json")]
public class GranularityController : ControllerBase
{
    private readonly MetadataService _metadataService;
    private readonly SystemService _systemService;

    public GranularityController(
        MetadataService metadataService,
        SystemService systemService)
    {
        _metadataService = metadataService;
        _systemService = systemService;
    }

    [HttpGet]
    [Route("api/granularity")]
    public ActionResult<IEnumerable<Granularity>> GetGranularity()
    {
        var granularities = _metadataService.GetGranularities();
        return Ok(granularities);
    }

    [HttpGet]
    [Route("api/granularity/{granularityId:guid}")]
    public ActionResult<Categorization> GetGranularity(Guid granularityId)
    {
        var granularity = _metadataService.GetGranularity(granularityId);

        return Ok(granularity);
    }

    [Authorize(Roles = "Administrator,ReadWrite")]
    [HttpPost]
    [Route("api/granularity")]
    public ActionResult SaveGranularity([FromBody] Granularity granularity)
    {
        _metadataService.Save(granularity);

        return Ok();
    }
}