using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Naiad.Libraries.ActivityPub.Models.Environment;
using Naiad.Libraries.ActivityPub.Models.General;
using Naiad.Libraries.ActivityPub.Models.NodeInfo;
using Naiad.Libraries.ActivityPub.Services;
using Naiad.Libraries.Core.Interfaces;
using System;
using Naiad.Libraries.Web.Helpers;

namespace Naiad.Modules.ActivityPub.Core.Controllers.General;

public class NodeInfoController : ControllerBase
{
    private readonly INaiadLogger _logger;
    private readonly ApService _apService;
    private readonly DomainName _domainName;

    public NodeInfoController(
        ApService apService,
        DomainName domainName,
        INaiadLogger logger)
    {
        _apService = apService;
        _domainName = domainName;
        _logger = logger;
    }

    [Route(".well-known/nodeinfo")]
    [HttpGet]
    public ActionResult<Link> GetLink()
    {

        var link = new NodeLink
        {
            Rel = "http://nodeinfo.diaspora.software/ns/schema/2.0",
            Href = new Uri($"https://{Environment.GetEnvironmentVariable("DOMAINNAME")}/nodeinfo/2.0")
        };

        _logger.Info("Called GetLink", User.GetUserId());


        return Ok(link);
    }

}
