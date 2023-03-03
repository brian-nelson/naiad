using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Models.DataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.System.Services;
using Naiad.Libraries.Web.Helpers;
using Naiad.Modules.Api.Core.Helpers;
using Naiad.Modules.Api.Core.Objects;

namespace Naiad.Modules.Api.Core.Controllers;

[Authorize]
[Produces("application/json")]
public class DataController : ControllerBase
{
    private readonly DataService _dataService;
    private readonly MetadataService _metadataService;
    private readonly INaiadLogger _logger;

    public DataController(
        DataService dataService,
        MetadataService metadataService,
        INaiadLogger logger)
    {
        _dataService = dataService;
        _metadataService = metadataService;
        _logger = logger;
    }

    [RequestSizeLimit(500_000_000)]
    [Authorize(Roles = "Administrator,ReadWrite")]
    [HttpPost]
    [Route("api/data/{*filePathAndName}")]
    public ActionResult<LoadDataResult> ImportData(
        [FromRoute] string filePathAndName, 
        [FromForm] IFormFile file)
    {
        // TODO Validate Filename

        if (file == null
            || filePathAndName.IsNullOrEmpty())
        {
            return BadRequest();
        }

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

        _logger.Info($"Data file saved ({filePathAndName},{pointer.Id})", User.GetUserId());

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

            using (var stream = _dataService.GetFile(fileInfo.Id))
            {
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                memoryStream.Position = 0;

                _logger.Info($"Data file retrieved ({filePathAndName})", User.GetUserId());

                return new FileStreamResult(memoryStream, "application/octet-stream")
                {
                    FileDownloadName = filename
                };
            }
        }

        _logger.Info($"Data file not found ({filePathAndName})", User.GetUserId());
        return new StatusCodeResult(StatusCodes.Status404NotFound);
    }

    [HttpGet]
    [Route("api/data/list/{*filePathAndName}")]
    public ActionResult<IEnumerable<NaiadFileInfo>> ListFiles([FromRoute] string filePathAndName)
    {
        var fileInfo = _dataService.ListFiles(filePathAndName);
        return Ok(fileInfo);
    }
}