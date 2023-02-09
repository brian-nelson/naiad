using System;
using Naiad.Libraries.Core.Helpers;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;
using System.Security.Cryptography;

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

    [Test]
    public void TestHashPasswordBadSalt()
    {
        var saltBytes = RandomNumberGenerator
            .GetBytes(40);

        var salt = Convert.ToBase64String(saltBytes);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            string hash = PasswordHelper.Hash(
                RandomHelper.GetRandomAlphaNumericString(20),
                salt);
        });
    }
}
