using System.Collections.Generic;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Interfaces.System;

namespace Naiad.Libraries.System.Managers;

public class ConnectorManager
{
    private readonly IConnectorConfigurationRepo _connectorConfigurationRepo;
    private readonly INaiadLogger _logger;
    private readonly IEnumerable<IConnector> _connectors;

    public ConnectorManager(
        IEnumerable<IConnector> connectors,
        IConnectorConfigurationRepo connectorConfigurationRepo,
        INaiadLogger logger)
    {
        _connectorConfigurationRepo = connectorConfigurationRepo;
        _connectors = connectors;
        _logger = logger;
    }



}