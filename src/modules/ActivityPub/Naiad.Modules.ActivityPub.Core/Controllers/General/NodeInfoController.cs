using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.ActivityPub.Models.Environment;
using Naiad.Libraries.ActivityPub.Models.General;
using Naiad.Libraries.ActivityPub.Models.NodeInfo;
using Naiad.Libraries.ActivityPub.Services;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.Web.Helpers;
using System.Collections.Generic;
using Naiad.Libraries.ActivityPub.Helpers;

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
            Href = UriHelper.GetNodeInfo(_domainName.Value)
        };

        _logger.Info("Called GetLink", User.GetUserId());

        return Ok(link);
    }

    [HttpGet("nodeinfo/2.0")]
    public ActionResult<NodeInfo> GetNodeInfo()
    {
        var nodeInfo = new NodeInfo
        {
            Version = "2.0",
            Software = new Software
            {
                Name = "Naiad",
                Version = "0.1"
            },
            Protocols = new[]
            {
                "activitypub"
            },
            Services = new Services
            {
                Outbound = new object[0],
                Inbound = new object[0]
            },
            Usage = new Usage
            {
                LocalPosts = 0,
                Users = new Users
                {
                    ActiveHalfyear = 1,
                    ActiveMonth = 1,
                    Total = 1
                }
            },
            OpenRegistrations = false,
            Metadata = new Dictionary<string, string>()
        };

        return Ok(nodeInfo);
    }
}
