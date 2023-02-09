using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;
using System.IO;
using Naiad.Libraries.Core.Helpers;

namespace Naiad.Libraries.Core.Tests.Helpers;

[TestFixture]
public class DirectoryHelperTests
{
    [Test]
    public void TestEnsureDirectory()
    { 
        string WorkingFolder = "\\Temp";
        string subFolder = Path.Combine(
            WorkingFolder,
            RandomHelper.GetRandomAlphaString(10));

        DirectoryHelper.EnsureDirectory(subFolder);

        DirectoryHelper.EnsureDirectory(subFolder);
    }
}
