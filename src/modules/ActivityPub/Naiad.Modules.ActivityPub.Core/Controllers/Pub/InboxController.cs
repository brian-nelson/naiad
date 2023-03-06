using System;
using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.ActivityPub.Models.Environment;
using Naiad.Libraries.ActivityPub.Models.Pub;
using Naiad.Libraries.ActivityPub.Services;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.ActivityPub.Constants;

namespace Naiad.Modules.ActivityPub.Core.Controllers.Pub;

public class InboxController 
    : ControllerBase
{
    private readonly INaiadLogger _logger;
    private readonly ApService _apService;
    private readonly DomainName _domainName;

    public InboxController(
        ApService apService,
        DomainName domainName,
        INaiadLogger logger)
    {
        _apService = apService;
        _domainName = domainName;
        _logger = logger;
    }

    [HttpGet]
    [Route("/Inbox/{userId:guid}")]
    public ActionResult<OrderedCollection<Post>> GetAllPostsInInbox(
        Guid userId)
    {
        
        var orderedCollection = new OrderedCollection<Post>
        {
            Summary = $"Inbox of {userId}",
            //OrderedItems = posts.OrderByDescending(i => i.Published)
        };

        return Ok(orderedCollection);
    }

    [HttpPost]
    public ActionResult SharedInbox(
        [FromBody] Activity activity)
    {
        if (activity == null)
        {
            return BadRequest("Activity can not be null!");
        }

        return Ok();
    }

    [HttpPost]
    [Route("/Inbox/{userId:guid}")]
    public ActionResult Inbox(
        Guid userId,
        [FromBody] Activity activity)
    {
        if (activity == null)
        {
            return BadRequest("Activity can not be null!");
        }

        switch (activity.Type)
        {
            case ActivityTypeConstants.Create:
                break;
            case ActivityTypeConstants.Follow:
                break;
            case ActivityTypeConstants.Accept:
                break;
            case ActivityTypeConstants.Announce:
                break;
            case ActivityTypeConstants.Like:
                break;
            case ActivityTypeConstants.Update:
                break;
            case ActivityTypeConstants.Undo:
                break;
            case ActivityTypeConstants.Delete:
                break;
        }

        return Ok();
    }
}
