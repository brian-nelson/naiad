using System.Collections.Generic;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Services;

namespace Naiad.Libraries.System.Managers;

public class HandlerManager : AbstractRunnableService
{
    private readonly SystemService _systemService;
    private readonly INaiadLogger _logger;
    private readonly IEnumerable<IHandler> _handlers;

    public HandlerManager(
        IEnumerable<IHandler> handlers,
        SystemService systemService,
        INaiadLogger logger)
    {
        _systemService = systemService;
        _handlers = handlers;
        _logger = logger;
    }


    public override void DoWork()
    {
        
    }
}