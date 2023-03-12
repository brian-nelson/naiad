using System;
using System.Collections.Generic;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Constants.System;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.System.Services;

namespace Naiad.Libraries.System.Managers;

public class EventHandlerManager : AbstractRunnableService
{
    private readonly SystemService _systemService;
    private readonly INaiadLogger _logger;
    private readonly IEnumerable<IHandler> _handlers;

    private DateTime _lastQueryTime;

    public EventHandlerManager(
        IEnumerable<IHandler> handlers,
        SystemService systemService,
        INaiadLogger logger)
    {
        _systemService = systemService;
        _handlers = handlers;
        _logger = logger;

        Initialize();
    }

    private void Initialize()
    {
        _lastQueryTime = DateTime.MinValue;

        var configEntry = _systemService.GetConfiguration(ConfigurationConstants.HANDLER_MANAGER_LAST_EVENT_TIME);

        if (configEntry != null)
        {
            DateTime.TryParse(configEntry.Value, out _lastQueryTime);
        }
    }


    public override void DoWork()
    {
        var now = DateTime.UtcNow;

        var systemEvents = _systemService.GetSystemEvents(_lastQueryTime, now);

        foreach (var systemEvent in systemEvents)
        {
            foreach (var handler in _handlers)
            {
                var eventReceipt = new SystemEventReceipt
                {
                    Time = now,
                    EventId = systemEvent.Id,
                    HandlerConfigurationId = handler.HandlerConfigurationId
                };

                try
                {
                    var result = handler.HandleEvent(systemEvent, out string details);

                    if (result != HandlerResults.NoInterest)
                    {
                        if (result == HandlerResults.Success)
                        {
                            eventReceipt.Result = HandlerResultConstants.SUCCESS;
                        }
                        else if (result == HandlerResults.Failure)
                        {
                            eventReceipt.Result = HandlerResultConstants.FAILURE;
                        }
                        else
                        {
                            eventReceipt.Result  = HandlerResultConstants.RETRY;
                        }
                        
                        eventReceipt.Details = details;
                    }

                }
                catch (Exception ex)
                {
                    eventReceipt.Result = HandlerResultConstants.EXCEPTION;
                    eventReceipt.Details = ex.Message;
                    _systemService.Save(eventReceipt);
                }
            }
        }

        _lastQueryTime = now;
    }

    public override void OnStopping()
    {
        var configEntry = _systemService.GetConfiguration(ConfigurationConstants.HANDLER_MANAGER_LAST_EVENT_TIME);

        if (configEntry == null)
        {
            configEntry = new Configuration
            {
                Key = ConfigurationConstants.HANDLER_MANAGER_LAST_EVENT_TIME,
                IsInternal = true
            };
        }

        configEntry.Value = _lastQueryTime.ToString("O");
        _systemService.Save(configEntry);
    }
}