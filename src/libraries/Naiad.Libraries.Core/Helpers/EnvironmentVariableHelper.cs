using System;
using System.Collections;
using System.Collections.Generic;

namespace Naiad.Libraries.Core.Helpers;

public static class EnvironmentVariableHelper
{
    public static Dictionary<string, string> GetMachineEnvVars(string prefix)
    {
        var output = new Dictionary<string, string>();

        var variables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine);

        foreach (DictionaryEntry entry in variables)
        {
            var key = entry.Key.ToString();

            if (prefix != null)
            {
                if (key != null
                    && key.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                {
                    var subkey = key.Substring(prefix.Length);

                    var value = entry.Value?.ToString();
                    output.Add(subkey, value);
                }
            }
            else
            {
                output.Add(key, entry.Value?.ToString());
            }
        }

        return output;
    }
}
