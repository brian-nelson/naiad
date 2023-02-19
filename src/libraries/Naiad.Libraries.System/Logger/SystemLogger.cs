using System;
using System.Runtime.CompilerServices;
using Naiad.Libraries.System.Constants.System;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.Logger
{
    public class SystemLogger : INaiadLogger
    {
        private readonly ILogEntryRepo _logEntryRepo;

        public SystemLogger(
            ILogEntryRepo logEntryRepo)
        {
            _logEntryRepo = logEntryRepo;
        }

        public void Debug(string message,
            Guid? userId = null)
        {
            Log(LogLevels.DEBUG, message, userId);
        }

        public void Info(string message,
            Guid? userId = null)
        {
            Log(LogLevels.INFO, message, userId);
        }

        public void Warn(
            string message,
            Guid? userId = null)
        {
            Log(LogLevels.WARN, message, userId);
        }

        public void Error(
            string message,
            Guid? userId = null)
        {
            Log(LogLevels.ERROR, message, userId);
        }

        public void Exception(
            Exception ex,
            Guid? userId = null)
        {
            Log(LogLevels.ERROR, ex.ToString(), userId);
        }

        private void Log(
            string level, 
            string message,
            Guid? userId)
        {
            //TODO add cache.

            var logEntry = new LogEntry
            {
                EntryDateTime = DateTimeOffset.UtcNow,
                Level = level,
                Message = message
            };

            _logEntryRepo.Save(logEntry);
        }
    }
}
