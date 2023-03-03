using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Naiad.Modules.Api.Core.Helpers;

public static class JwtHelper
{
    public static string CreateJwt(
        Dictionary<string, string> sessionInfo,
        string jwtSecret,
        int daysValid = 1)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = CreateClaimsIdentities(sessionInfo);

        var now = DateTime.UtcNow;

        // Create JWToken
        var token = tokenHandler.CreateJwtSecurityToken(
            subject: claims,
            notBefore: now,
            expires: now.AddDays(daysValid),
            signingCredentials:
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Convert.FromBase64String(jwtSecret)),
                SecurityAlgorithms.HmacSha256Signature));

        return tokenHandler.WriteToken(token);
    }

    public static ClaimsIdentity CreateClaimsIdentities(Dictionary<string, string> sessionInfo)
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity();

        foreach (var key in sessionInfo.Keys)
        {
            string value = sessionInfo[key];
            claimsIdentity.AddClaim(new Claim(key, value));
        }

        return claimsIdentity;
    }

    public static string CreateJwtSecret()
    {
        var hmac = new HMACSHA256();
        var key = Convert.ToBase64String(hmac.Key);

        return key;
    }
}
