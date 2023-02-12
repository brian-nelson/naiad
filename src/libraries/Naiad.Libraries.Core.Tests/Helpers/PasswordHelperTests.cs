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

    /// <summary>
    /// Tests to make sure all characters are used when generating passwords.
    /// </summary>
    [Test]
    public void TestGeneratePassword()
    {
        var passwordChars = PasswordHelper.Characters.Length;

        bool[] hasChar = new bool[passwordChars];

        for (int i = 0; i < 1000; i++)
        {
            var password = PasswordHelper.GeneratePassword(20);

            for (int j = 0; j < passwordChars; j++)
            {
                if (!hasChar[j])
                {
                    var c = PasswordHelper.Characters[j];

                    if (password.Contains(c))
                    {
                        hasChar[j] = true;
                    }
                }
            }
        }

        for (int i = 0; i < passwordChars; i++)
        {
            Assert.IsTrue(hasChar[i]);
        }
    }
}
