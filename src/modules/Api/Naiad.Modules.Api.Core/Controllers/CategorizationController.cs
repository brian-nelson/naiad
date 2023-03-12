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
public class CategorizationController : ControllerBase
{
    private readonly MetadataService _metadataService;
    private readonly INaiadLogger _logger;

    public CategorizationController(
        MetadataService metadataService,
        INaiadLogger logger)
    {
        _metadataService = metadataService;
        _logger = logger;
    }

    [HttpGet]
    [Route("api/categorizations")]
    public ActionResult<IEnumerable<Categorization>> GetCategorizations()
    {
        var categorizations = _metadataService.GetCategorizations();

        _logger.Info("All categorizations retrieved", User.GetUserId());

        return Ok(categorizations);
    }

    [HttpGet]
    [Route("api/categorization/{categorizationId:guid}")]
    public ActionResult<Categorization> GetCategorization(Guid categorizationId)
    {
        var categorization = _metadataService.GetCategorization(categorizationId);

        _logger.Info($"Categorization retrieved ({categorizationId})", User.GetUserId());

        return Ok(categorization);
    }

    [Authorize(Roles = "Administrator,ReadWrite")]
    [HttpPost]
    [Route("api/categorization")]
    public ActionResult SaveCategorization([FromBody] Categorization categorization)
    {
        _metadataService.Save(categorization);

        _logger.Info($"Categorization saved ({categorization.Id})", User.GetUserId());

        return Ok();
    }
}
