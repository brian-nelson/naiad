using System;
using Naiad.Libraries.ActivityPub.Helpers;
using Naiad.Libraries.ActivityPub.Models.Environment;
using Naiad.Libraries.ActivityPub.Models.General;
using Naiad.Libraries.ActivityPub.Models.Pub;
using Naiad.Libraries.System.Services;

namespace Naiad.Libraries.ActivityPub.Services;

public class ApService
{
    private readonly DomainName _domainName;
    private readonly SystemService _systemService;

    public ApService(
        DomainName domainName,
        SystemService systemService)
    {
        _domainName = domainName;
        _systemService = systemService;
    }

    /// <summary>
    /// This method will dynamically generate a webfinger record for the user
    /// if they do not have one.  If a username is set in the system
    /// service (userRepo)
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    public Webfinger GetWebfingerBySubject(string subject)
    {
        // Check to see if a user exists
        string username = ActorHelper.GetUsername(subject, _domainName.Value);

        if (username != null)
        {
            var user = _systemService.GetUserByUsername(username);

            if (user != null)
            {
                return WebfingerHelper.BuildWebfinger(
                    _domainName.Value,
                    user.Id,
                    user.Username);
            }
        }

        return null;
    }

    public Actor GetActor(Guid actorId)
    {
        var user = _systemService.GetUser(actorId);

        if (user != null)
        {
            var actor = ActorHelper.CreateActor(user, _domainName.Value);
            return actor;
        }

        return null;
    }


}