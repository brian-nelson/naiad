using System;
using Naiad.Libraries.System.Constants.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.Interfaces.System;

public interface IHandler
{
    /// <summary>
    /// Handles events as they are sent.  Assumes clean exit is successful.
    /// </summary>
    /// <param name="systemEvent"></param>
    /// <param name="result"></param>
    /// <returns>Any Result text.  Default to null if not required.</returns>
    public HandlerResults HandleEvent(SystemEvent systemEvent, out string details);


    public Guid HandlerConfigurationId { get; }
}