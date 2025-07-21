using PowerDNSManagerCLI.Models;

namespace PowerDNSManagerCLI.Services;

public static class ConfigParser
{
    private const string ConfigPath = "/etc/powerdns/pdns.conf";

    public static string? GetApiKey()
    {
        if (!File.Exists(ConfigPath))
            return null;

        foreach (var line in File.ReadLines(ConfigPath))
        {
            if (line.StartsWith('#') || !line.Contains("="))
                continue;

            var parts = line.Split('=', 2, StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
                continue;

            if (parts[0] == "api-key")
                return parts[1];
        }

        return null;
    }

    public static IEnumerable<ConfigEntry> GetAllConfig()
    {
        if (!File.Exists(ConfigPath))
            yield break;

        foreach (var line in File.ReadLines(ConfigPath))
        {
            if (line.StartsWith('#') || !line.Contains("="))
                continue;

            var parts = line.Split('=', 2, StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
                continue;

            yield return new ConfigEntry
            {
                Key = parts[0],
                Value = parts[1]
            };
        }
    }
}
