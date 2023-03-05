using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.ActivityPub.Models.Environment;
using Naiad.Libraries.ActivityPub.Models.Pub;
using Naiad.Libraries.ActivityPub.Services;
using Naiad.Libraries.Core.Interfaces;
using System.Threading.Tasks;
using System;

namespace Naiad.Modules.ActivityPub.Core.Controllers.Pub;

public class ActorController 
    : ControllerBase
{
    private readonly INaiadLogger _logger;
    private readonly ApService _apService;
    private readonly DomainName _domainName;

    public ActorController(
        ApService apService,
        DomainName domainName,
        INaiadLogger logger)
    {
        _apService = apService;
        _domainName = domainName;
        _logger = logger;
    }

    [HttpGet]
    [Route("/Actor/{actorId:guid}")]
    public async Task<ActionResult<Actor>> GetActor(
        [FromRoute] Guid actorId)
    {
        var actor = _apService.GetActor(actorId);

        if (actor == null)
        {
            _logger.Warn($"{nameof(actor)} is null");
            return BadRequest($"No actor found for id: {actorId}");
        }

        _logger.Info($"{nameof(actor)} is null");
        return Ok(actor);
    }
}