using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.Core.Helpers;
using Naiad.Libraries.System.Constants.DataManagement;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Services;
using Naiad.Modules.Api.Core.Helpers;
using Naiad.Modules.Api.Core.Objects;

namespace Naiad.Modules.Api.Core.Controllers;

[Authorize]
[Produces("application/json")]
public class StructuredDataController : ControllerBase
{
    private readonly MetadataService _metadataService;
    private readonly StructuredDataService _structuredDataService;
    private readonly INaiadLogger _logger;

    public StructuredDataController(
        MetadataService metadataService,
        StructuredDataService structuredDataService,
        INaiadLogger logger)
    {
        _metadataService = metadataService;
        _structuredDataService = structuredDataService;
        _logger = logger;
    }

    [Authorize(Roles = "Administrator,ReadWrite")]
    [HttpPost]
    [Route("api/definition")]
    public void DefineDataType(
        [FromBody] StructuredDataDto structuredDataDto)
    {
        var sdd = structuredDataDto.ToStructuredDataDefinition();
        _metadataService.DefineStructuredData(sdd);
        _logger.Info($"SDD defined ({sdd.Name})", User.GetUserId());
    }

    [HttpGet]
    [Route("api/definition/{name}")]
    public ActionResult<StructuredDataDto> GetDataType(
        [FromRoute] string name)
    {
        var sdd = _metadataService.GetStructuredDataDefinition(name);
        _logger.Info($"SDD retrieved ({name})", User.GetUserId());

        return Ok(sdd?.ToStructuredDataDto());
    }

    [HttpGet]
    [Route("api/definitions")]
    public ActionResult<IEnumerable<StructuredDataDto>> GetDataTypes()
    {
        var sdds = _metadataService.GetStructuredDataDefinitions();
        _logger.Info($"All SDDs retrieved", User.GetUserId());

        return Ok(sdds.ToStructuredDataDtos());
    }

    [HttpGet]
    [Route("api/data/{dataPointerId:guid}/definitions")]
    public ActionResult<IEnumerable<StructuredDataDto>> GetDataTypeUsed(
        [FromRoute] Guid dataPointerId)
    {
        var sdds = _metadataService.GetStructuredDataDefinitionsTaggedToData(dataPointerId);
        _logger.Info($"All SDDs retrieved", User.GetUserId());

        return Ok(sdds.ToStructuredDataDtos());
    }

    [HttpPost]
    [Route("api/data/{dataPointerId:guid}/transform/{metadataId:guid}")]
    public ActionResult TransformData(
        [FromRoute] Guid dataPointerId,
        [FromRoute] Guid metadataId)
    {
        _structuredDataService.TransformFileToStructuredData(dataPointerId, metadataId);
        return Ok();
    }

    [HttpGet]
    [Route("api/structured/{metadataId:guid}")]
    public ContentResult GetAllStructuredData(
        [FromRoute] Guid metadataId)
    {
        var dataTable = _structuredDataService.GetAllData(metadataId);

        var json = JsonHelper.ToJson(dataTable);

        return this.Content(json, MimeTypeConstants.JSON);
    }
}
