using System.Collections.Generic;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Services;

namespace Naiad.Libraries.System.Managers;

public class ConnectorManager
{
    private readonly SystemService _systemService;
    private readonly INaiadLogger _logger;
    private readonly IEnumerable<IConnector> _connectors;

    public ConnectorManager(
        SystemService systemService,
        IEnumerable<IConnector> connectors,
        INaiadLogger logger)
    {
        _systemService = systemService;
        _connectors = connectors;
        _logger = logger;
    }



}