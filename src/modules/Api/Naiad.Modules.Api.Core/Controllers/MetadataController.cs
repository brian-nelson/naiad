using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.System.Services;
using Naiad.Modules.Api.Core.Services;

namespace Naiad.Modules.Api.Core.Controllers;

[Authorize]
[Produces("application/json")]
public class MetadataController : ControllerBase
{
    private readonly MetadataUIService _metadataUiService;
    private readonly MetadataService _metadataService;
    private readonly INaiadLogger _logger;

    public MetadataController(
        MetadataUIService metadataUiService,
        MetadataService metadataService,
        INaiadLogger logger)
    {
        _metadataUiService = metadataUiService;
        _metadataService = metadataService;
        _logger = logger;
    }

    [HttpGet]
    [Route("api/datapointer/{dataPointerId:guid}")]
    public ActionResult<DataPointer> GetDataPointer(
        [FromRoute] Guid dataPointerId)
    {
        var pointer = _metadataService.GetDataPointer(dataPointerId);
        return Ok(pointer);
    }

    [HttpPost]
    [Route("api/datapointer")]
    public ActionResult SaveDataPointer([FromBody] DataPointer dataPointer)
    {
        _metadataService.Save(dataPointer);
        return Ok();
    }

    
}
