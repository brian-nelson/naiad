using System;
using System.Collections.Generic;
using Naiad.Libraries.Testing.Helpers;
using Naiad.Libraries.Web.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.Web.Tests.Helpers
{
    [TestFixture]
    public class JwtHelperTests
    {
        [Test]
        public void TestCreateJwtSecret()
        {
            var secret = JwtHelper.CreateJwtSecret();
            Assert.IsNotNull(secret);
        }

        [Test]
        public void TestCreateJwt()
        {
            var secret = JwtHelper.CreateJwtSecret();

            var sessionInfo = new Dictionary<string, string>
            {
                { "UserId", Guid.NewGuid().ToString() },
                { "SessionId", Guid.NewGuid().ToString() },
                { "FamilyName", RandomHelper.GetRandomAlphaString(10) },
                { "GivenName", RandomHelper.GetRandomAlphaString(10) },

            };

            string jwt = JwtHelper.CreateJwt(
                sessionInfo, 
                secret, 
                1);

            Assert.IsNotNull(jwt);
        }


    }
}
