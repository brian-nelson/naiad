using Microsoft.AspNetCore.Mvc;
using Naiad.Libraries.ActivityPub.Models.Environment;
using Naiad.Libraries.ActivityPub.Services;
using Naiad.Libraries.Core.Interfaces;

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



}
