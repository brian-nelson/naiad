using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    private readonly SystemService _systemService;

    public MetadataController(
        MetadataUIService metadataUiService,
        MetadataService metadataService,
        SystemService systemService)
    {
        _metadataUiService = metadataUiService;
        _metadataService = metadataService;
        _systemService = systemService;
    }

    

    

    // Zones
}
