using System;
using System.Security.Claims;

namespace Naiad.Libraries.Web.Helpers;

public static class UserHelper
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst("UserId");

        return Guid.Parse(claim.Value);
    }

    public static Guid GetSessionId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst("SessionId");

        return Guid.Parse(claim.Value);
    }

    public static string GetEmail(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.Email);

        return claim?.Value;
    }

    public static string GetRole(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.Role);

        return claim?.Value;
    }
}

