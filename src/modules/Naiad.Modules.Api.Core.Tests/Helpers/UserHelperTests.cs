using System.Security.Claims;
using Naiad.Libraries.System.Constants.System;
using Naiad.Libraries.Testing.Helpers;
using Naiad.Modules.Api.Core.Helpers;
using NUnit.Framework;

namespace Naiad.Modules.Api.Core.Tests.Helpers
{
    [TestFixture]
    public class UserHelperTests
    {
        [Test]
        public void TestGetUserId()
        {
            Guid userId = Guid.NewGuid();

            var sessionInfo = new Dictionary<string, string>
            {
                { "UserId", userId.ToString() },
                { "SessionId", Guid.NewGuid().ToString() },
                { "FamilyName", RandomHelper.GetRandomAlphaString(10) },
                { "GivenName", RandomHelper.GetRandomAlphaString(10) },
                { ClaimTypes.Role, Enum.GetName(typeof(UserTypes), UserTypes.ReadWrite) }
            };

            var ci = JwtHelper.CreateClaimsIdentities(sessionInfo);

            var cp = new ClaimsPrincipal(ci);
            var test = cp.GetUserId();

            Assert.AreEqual(userId, test);
        }

        [Test]
        public void TestGetSessionId()
        {
            Guid sessionId = Guid.NewGuid();

            var sessionInfo = new Dictionary<string, string>
            {
                { "UserId", Guid.NewGuid().ToString() },
                { "SessionId", sessionId.ToString() },
                { "FamilyName", RandomHelper.GetRandomAlphaString(10) },
                { "GivenName", RandomHelper.GetRandomAlphaString(10) },
                { ClaimTypes.Role, Enum.GetName(typeof(UserTypes), UserTypes.ReadWrite) }
            };

            var ci = JwtHelper.CreateClaimsIdentities(sessionInfo);

            var cp = new ClaimsPrincipal(ci);
            var test = cp.GetSessionId();

            Assert.AreEqual(sessionId, test);
        }

        [Test]
        public void TestGetEmail()
        {
            string email = RandomHelper.GetRandomAlphaString(10);

            var sessionInfo = new Dictionary<string, string>
            {
                { "UserId", Guid.NewGuid().ToString() },
                { "SessionId", Guid.NewGuid().ToString() },
                { ClaimTypes.Email, email },
                { "GivenName", RandomHelper.GetRandomAlphaString(10) },
                { ClaimTypes.Role, Enum.GetName(typeof(UserTypes), UserTypes.ReadWrite) }
            };

            var ci = JwtHelper.CreateClaimsIdentities(sessionInfo);

            var cp = new ClaimsPrincipal(ci);
            var test = cp.GetEmail();

            Assert.AreEqual(email, test);
        }

        [Test]
        public void TestGetRole()
        {
            string enumName = Enum.GetName(typeof(UserTypes), UserTypes.ReadWrite);

            var sessionInfo = new Dictionary<string, string>
            {
                { "UserId", Guid.NewGuid().ToString() },
                { "SessionId", Guid.NewGuid().ToString() },
                { "Email", RandomHelper.GetRandomAlphaString(10) },
                { "GivenName", RandomHelper.GetRandomAlphaString(10) },
                { ClaimTypes.Role, enumName}
            };

            var ci = JwtHelper.CreateClaimsIdentities(sessionInfo);

            var cp = new ClaimsPrincipal(ci);
            var test = cp.GetRole();

            Assert.AreEqual(enumName, test);
        }
    }
}
