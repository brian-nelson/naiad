using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.ActivityPub.Models.Environment;
using Naiad.Libraries.ActivityPub.Models.General;
using Naiad.Libraries.ActivityPub.Services;
using Naiad.Libraries.Core.Interfaces;

namespace Naiad.Modules.ActivityPub.Core.Controllers.General;

public class WellKnownController 
    : ControllerBase
{
    private readonly INaiadLogger _logger;
    private readonly ApService _apService;
    private readonly DomainName _domainName;

    public WellKnownController(
        ApService apService,
        DomainName domainName,
        INaiadLogger logger)
    {
        _apService = apService;
        _domainName = domainName;
        _logger = logger;
    }

    [HttpGet]
    [Route(".well-known/webfinger")]
    public ActionResult<Webfinger> GetWebfinger(
        [FromQuery] string resource)
    {
        var finger = _apService.GetWebfingerBySubject(resource);

        if (finger == null)
        {
            return BadRequest("Not found WebFinger.");
        }

        return Ok(finger);
    }
}
