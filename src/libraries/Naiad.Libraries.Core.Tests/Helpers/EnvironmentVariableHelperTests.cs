using Naiad.Libraries.Core.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.Core.Tests.Helpers;

[TestFixture]
public class EnvironmentVariableHelperTests
{
    [Test]
    public void TestGetMachineEnvVarsWithNull()
    {
        var envVars = EnvironmentVariableHelper.GetMachineEnvVars(null);

        Assert.Greater(envVars.Count, 0);
    }

    [Test]
    public void TestGetMachineEnvVars()
    {
        var envVars = EnvironmentVariableHelper.GetMachineEnvVars("naiad");

        Assert.Greater(envVars.Count, 0);
    }
}
