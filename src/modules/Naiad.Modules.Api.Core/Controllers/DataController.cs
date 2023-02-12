using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.System.Services;
using Naiad.Modules.Api.Core.Objects;

namespace Naiad.Modules.Api.Core.Controllers;

[Authorize]
[Produces("application/json")]
public class DataController : ControllerBase
{
    private readonly SystemService _systemService;
    private readonly DataService _dataService;
    private readonly MetadataService _metadataService;

    public DataController(
        SystemService systemService,
        DataService dataService,
        MetadataService metadataService)
    {
        _systemService = systemService;
        _dataService = dataService;
        _metadataService = metadataService;
    }

    [Authorize(Roles = "Administrator,ReadWrite")]
    [HttpPost]
    [Route("api/data/{*filePathAndName}")]
    public ActionResult<LoadDataResult> ImportData(
        [FromRoute] string filePathAndName, 
        [FromForm] IFormFile file)
    {
        // TODO Validate Filename

        using (var ms = new MemoryStream())
        {
            file.CopyTo(ms);

            ms.Position = 0;

            _dataService.SaveFile(filePathAndName, ms);
        }

        var pointer = _metadataService.GetDataPointer(filePathAndName);
        if (pointer == null)
        {
            // TODO - Possibly have default Zone and Granularity configuration
            pointer = new DataPointer
            {
                StorageLocation = filePathAndName
            };
            _metadataService.Save(pointer);
        }

        var result = new LoadDataResult
        {
            DataPointerId = pointer.Id,
            FileId = filePathAndName
        };

        return Ok(result);
    }

    [HttpGet]
    [Route("api/data/{*filePathAndName}")]
    public IActionResult GetData(
        [FromRoute] string filePathAndName)
    {
        var fileInfo = _dataService.GetFileInfo(filePathAndName);

        if (fileInfo != null)
        {
            var filename = Path.GetFileName(fileInfo.Filename) ?? filePathAndName;
            var stream = _dataService.GetFile(fileInfo.Id);

            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = filename
            };
        }

        return new StatusCodeResult(StatusCodes.Status404NotFound);
    }
}