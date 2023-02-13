using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.System.Services;
using System.Collections.Generic;
using System;

namespace Naiad.Modules.Api.Core.Controllers; 

[Authorize]
[Produces("application/json")]
public class ZoneController : ControllerBase
{
    private readonly MetadataService _metadataService;
    private readonly SystemService _systemService;

    public ZoneController(
        MetadataService metadataService,
        SystemService systemService)
    {
        _metadataService = metadataService;
        _systemService = systemService;
    }

    [HttpGet]
    [Route("api/zone")]
    public ActionResult<IEnumerable<Zone>> GetZones()
    {
        var zones = _metadataService.GetZones();
        return Ok(zones);
    }

    [HttpGet]
    [Route("api/zone/{zoneId:guid}")]
    public ActionResult<Zone> GetZone(Guid zoneId)
    {
        var zone = _metadataService.GetZone(zoneId);

        return Ok(zone);
    }

    [Authorize(Roles = "Administrator,ReadWrite")]
    [HttpPost]
    [Route("api/zone")]
    public ActionResult SaveZone([FromBody] Zone zone)
    {
        _metadataService.Save(zone);

        return Ok();
    }

}

