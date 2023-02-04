using System;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.Testing.Helpers;
using NUnit.Framework;

namespace Naiad.Libraries.System.Tests.Models.System;

[TestFixture]
internal class LogEntryTests
{
    [Test]
    public void TestGetSet()
    {
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var entryDateTime = RandomHelper.GetRandomDateTimeOffset(30);
        var level = RandomHelper.GetRandomAlphaString(5);
        var message = RandomHelper.GetRandomAlphaNumericString(100);

        var le = new LogEntry
        {
            Id = id,
            UserId = userId,
            EntryDateTime = entryDateTime,
            Level = level,
            Message = message
        };

        Assert.AreEqual(id, le.Id);
        Assert.AreEqual(userId, le.UserId);
        Assert.AreEqual(entryDateTime, le.EntryDateTime);
        Assert.AreEqual(level, le.Level);
        Assert.AreEqual(message, le.Message);

        le.UserId = null;
        Assert.IsNull(le.UserId);
    }
}

