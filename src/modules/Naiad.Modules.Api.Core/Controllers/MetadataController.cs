using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.System.Interfaces;
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

    
}
