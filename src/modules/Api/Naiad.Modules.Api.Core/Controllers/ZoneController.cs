using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.System.Services;
using System.Collections.Generic;
using System;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.Web.Helpers;

namespace Naiad.Modules.Api.Core.Controllers; 

[Authorize]
[Produces("application/json")]
public class ZoneController : ControllerBase
{
    private readonly MetadataService _metadataService;
    private readonly INaiadLogger _logger;

    public ZoneController(
        MetadataService metadataService,
        INaiadLogger logger)
    {
        _metadataService = metadataService;
        _logger = logger;
    }

    [HttpGet]
    [Route("api/zones")]
    public ActionResult<IEnumerable<Zone>> GetZones()
    {
        var zones = _metadataService.GetZones();
        _logger.Info($"All zones retrieved", User.GetUserId());
        return Ok(zones);
    }

    [HttpGet]
    [Route("api/zone/{zoneId:guid}")]
    public ActionResult<Zone> GetZone(Guid zoneId)
    {
        var zone = _metadataService.GetZone(zoneId);
        _logger.Info($"Zone retrieved ({zoneId})", User.GetUserId());
        return Ok(zone);
    }

    [Authorize(Roles = "Administrator,ReadWrite")]
    [HttpPost]
    [Route("api/zone")]
    public ActionResult SaveZone([FromBody] Zone zone)
    {
        _metadataService.Save(zone);
        _logger.Info($"Zone saved ({zone.Id})", User.GetUserId());
        return Ok();
    }

}

