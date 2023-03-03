using System;

namespace Naiad.Libraries.Core.Interfaces;

public interface INaiadLogger
{
    public void Debug(string message, Guid? userId = null);
    public void Info(string message, Guid? userId = null);
    public void Warn(string message, Guid? userId = null);
    public void Error(string message, Guid? userId = null);
    public void Exception(Exception ex, Guid? userId = null);
}