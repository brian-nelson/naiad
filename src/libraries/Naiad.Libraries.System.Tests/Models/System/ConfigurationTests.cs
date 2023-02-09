using System;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.System
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void TestGetSet()
        {
            var id = Guid.NewGuid();
            var key = RandomHelper.GetRandomAlphaNumericString(20);
            var value = RandomHelper.GetRandomAlphaString(20);

            var configuration = new Configuration
            {
                Id = id,
                Key = key,
                Value = value
            };

            Assert.AreEqual(id, configuration.Id);
            Assert.AreEqual(key, configuration.Key);
            Assert.AreEqual(value, configuration.Value);
        }
    }
}
