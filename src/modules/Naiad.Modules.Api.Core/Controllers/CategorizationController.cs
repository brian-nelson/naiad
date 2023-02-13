using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.System.Services;
using System.Collections.Generic;
using System;

namespace Naiad.Modules.Api.Core.Controllers;

[Authorize]
[Produces("application/json")]
public class CategorizationController : ControllerBase
{
    private readonly MetadataService _metadataService;
    private readonly SystemService _systemService;

    public CategorizationController(
        MetadataService metadataService,
        SystemService systemService)
    {
        _metadataService = metadataService;
        _systemService = systemService;
    }

    [HttpGet]
    [Route("api/categorization")]
    public ActionResult<IEnumerable<Categorization>> GetCategorizations()
    {
        var categorizations = _metadataService.GetCategorizations();
        return Ok(categorizations);
    }

    [HttpGet]
    [Route("api/categorization/{categorizationId:guid}")]
    public ActionResult<Categorization> GetCategorization(Guid categorizationId)
    {
        var categorization = _metadataService.GetCategorization(categorizationId);

        return Ok(categorization);
    }

    [Authorize(Roles = "Administrator,ReadWrite")]
    [HttpPost]
    [Route("api/categorization")]
    public ActionResult SaveCategorization([FromBody] Categorization categorization)
    {
        _metadataService.Save(categorization);

        return Ok();
    }
}
