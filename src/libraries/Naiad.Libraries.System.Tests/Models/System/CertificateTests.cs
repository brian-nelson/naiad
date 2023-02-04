using System;
using Naiad.Libraries.System.Constants.System;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.System;

[TestFixture]
public class CertificateTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var certificateContents = RandomHelper.GetRandomAlphaNumericString(100);
        var certificateType = CertificateTypes.PublicKey;
        var validFrom = RandomHelper.GetRandomDateTimeOffset(30);
        var validTo = RandomHelper.GetRandomDateTimeOffset(30);

        var c = new Certificate
        {
            Id = id,
            CertificateContents = certificateContents,
            CertificateType = certificateType,
            IsValid = true,
            ValidFrom = validFrom,
            ValidTo = validTo
        };

        Assert.AreEqual(id, c.Id);
        Assert.AreEqual(certificateContents, c.CertificateContents);
        Assert.AreEqual(certificateType, c.CertificateType);
        Assert.IsTrue(c.IsValid);
        Assert.AreEqual(validFrom, c.ValidFrom);
        Assert.AreEqual(validTo, c.ValidTo);

        c.IsValid = false;
        Assert.IsFalse(c.IsValid);

        certificateType = CertificateTypes.PrivateKey;
        c.CertificateType = certificateType;
        Assert.AreEqual(certificateType, c.CertificateType);
    }
}

