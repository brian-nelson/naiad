using Naiad.Libraries.Core.Helpers;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.Core.Tests.Helpers;

[TestFixture]
public class PasswordHelperTests
{
    [Test]
    public void TestGenerateSalt()
    {
        string salt = PasswordHelper.GenerateSalt();
        Assert.NotNull(salt);
    }

    [Test]
    public void TestHashPassword()
    {
        string salt = PasswordHelper.GenerateSalt();

        string hash = PasswordHelper.Hash(
            RandomHelper.GetRandomAlphaNumericString(20), salt);
    }
}
