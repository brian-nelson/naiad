using System;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.ActivityPub.Models.Environment;
using Naiad.Libraries.ActivityPub.Models.Pub;
using Naiad.Libraries.ActivityPub.Services;
using Naiad.Libraries.Core.Interfaces;

namespace Naiad.Modules.ActivityPub.Core.Controllers.Pub;

public class OutboxController : ControllerBase
{
    private readonly INaiadLogger _logger;
    private readonly ApService _apService;
    private readonly DomainName _domainName;

    public OutboxController(
        ApService apService,
        DomainName domainName,
        INaiadLogger logger)
    {
        _apService = apService;
        _domainName = domainName;
        _logger = logger;
    }

    [Route("/Outbox/{userId:guid}")]
    [HttpGet]
    public ActionResult<OrderedCollection<Post>> GetAllPublicPosts(
        Guid userId)
    {
        // // This filter can not use the extensions method IsPostPublic
        // var filterDefinitionBuilder = Builders<Post>.Filter;
        // var filter = filterDefinitionBuilder.Where(i => i.To.Any(item =>
        //     item == "https://www.w3.org/ns/activitystreams#Public"
        //     || item == "as:Public" || item == "public"));
        // var posts = await _repository.GetSpecificItems(filter, DatabaseLocations.OutboxNotes.Database,
        //     DatabaseLocations.OutboxNotes.Collection);

        var orderedCollection = new OrderedCollection<Post>
        {
            Summary = $"Posts of {userId}",
            //OrderedItems = posts
        };

        return Ok(orderedCollection);
    }
}
