using System;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;
using AssertionHelper = Naiad.Libraries.Testing.Helpers.AssertionHelper;

namespace Naiad.Libraries.System.Tests.Models.System
{
    [TestFixture]
    public class SessionTests
    {
        [Test]
        public void TestGetSet()
        {
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var createdOnDateTime = RandomHelper.GetRandomDateTimeOffset(10);
            var expiresOnDateTime = RandomHelper.GetRandomDateTimeOffset(10);

            var session = new Session()
            {
                Id = id,
                UserId = userId,
                CreatedOnDateTime = createdOnDateTime,
                ExpiresOnDateTime = expiresOnDateTime,
                IsDeleted = true
            };

            Assert.AreEqual(id, session.Id);
            Assert.AreEqual(userId, session.UserId);
            AssertionHelper.ApproximatelyEqual(createdOnDateTime, session.CreatedOnDateTime);
            AssertionHelper.ApproximatelyEqual(expiresOnDateTime, session.ExpiresOnDateTime);
            Assert.IsTrue(session.IsDeleted);

            session.IsDeleted = false;
            Assert.IsFalse(session.IsDeleted);

        }
    }
}
