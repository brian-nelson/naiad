using Naiad.Libraries.System.Constants.System;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Constants.System;

[TestFixture]
public class CertificateTypeTests
{
    [Test]
    public void TestCertificateTypeValues()
    {
        Assert.AreEqual(0, (int)CertificateTypes.PrivateKey);
        Assert.AreEqual(1, (int)CertificateTypes.PublicKey);
    }
}

