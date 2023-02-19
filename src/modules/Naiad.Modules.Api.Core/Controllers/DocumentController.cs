using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Models.DataManagement;
using Naiad.Libraries.System.Services;
using Naiad.Modules.Api.Core.Helpers;

namespace Naiad.Modules.Api.Core.Controllers;

[Authorize]
[Produces("application/json")]
public class DocumentController : ControllerBase
{
    private readonly MetadataService _metadataService;
    private readonly INaiadLogger _logger;

    public DocumentController(
        MetadataService metadataService,
        INaiadLogger logger)
    {
        _metadataService = metadataService;
        _logger = logger;
    }

    [Authorize(Roles = "Administrator,ReadWrite")]
    [HttpPost]
    [Route("api/definition")]
    public void DefineDataType([FromBody] StructuredDataDefinition structuredDataDefinition)
    {
        _metadataService.DefineStructuredData(structuredDataDefinition);
        _logger.Info($"SDD defined ({structuredDataDefinition.Name})", User.GetUserId());
    }

    [HttpGet]
    [Route("api/definition/{name}")]
    public ActionResult<StructuredDataDefinition> GetDataType([FromRoute] string name)
    {
        var sdd = _metadataService.GetStructuredDataDefinition(name);
        _logger.Info($"SDD retrieved ({name})", User.GetUserId());
        return Ok(sdd);
    }
}
