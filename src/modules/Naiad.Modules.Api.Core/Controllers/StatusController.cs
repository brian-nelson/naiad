using System;
using Microsoft.AspNetCore.Mvc;

namespace Naiad.Modules.Api.Core.Controllers;

[Produces("text/plain")]
public class StatusController : ControllerBase
{
    [HttpGet]
    [Route("/api")]
    public string Get()
    {
        return $"NAIAD Api Server - {DateTime.UtcNow}";
    }
}

