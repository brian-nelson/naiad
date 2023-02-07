using System.Collections.Generic;
using Naiad.Libraries.Core.Objects;
using NUnit.Framework;

namespace Naiad.Libraries.Core.Tests.Objects;

[TestFixture]
public class ConfigTests
{
    [Test]
    public void TestConfig()
    {
        Dictionary<string, string> testEnvVars = new Dictionary<string, string>
        {
            { "one", "testOne" },
            { "two", "2" },
            { "three", null }
        };

        var config = new Config(testEnvVars);
        
        var oneResult = config.GetString("one");
        Assert.AreEqual("testOne", oneResult);
        
        var twoResult = config.GetInt("two");
        Assert.IsTrue(twoResult.HasValue);
        Assert.AreEqual(2, twoResult.Value);

        var threeResultNull = config.GetInt("three");
        Assert.IsFalse(threeResultNull.HasValue);

        var threeResult = config.GetInt("three", 3);
        Assert.AreEqual(3, threeResult);
    }
}
