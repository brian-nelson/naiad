using System;
using System.Collections.Generic;

namespace Naiad.Libraries.Core.Objects;

public class Config
{
    private readonly Dictionary<string, string> _envVars;

    public Config(Dictionary<string, string> envVars)
    {
        _envVars = envVars;
    }

    public string GetString(string key)
    {
        if (_envVars.ContainsKey(key))
        {
            return _envVars[key];
        }

        return null;
    }

    public int? GetInt(string key)
    {
        if (_envVars.ContainsKey(key))
        {
            var value = _envVars[key];

            if (value != null)
            {
                return Convert.ToInt32(_envVars[key]);
            }
        }

        return null;
    }

    public int GetInt(string key, int defaultValue)
    {
        var value = GetInt(key);

        if (value.HasValue)
        {
            return value.Value;
        }

        return defaultValue;
    }
}
