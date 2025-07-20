namespace PowerDNSManagerCLI;

public static class ConfigHelper
{
    public static string? GetApiKey(string path = "/etc/pdns/pdns.conf")
    {
        if (!File.Exists(path)) return null;

        foreach (var line in File.ReadAllLines(path))
        {
            if (line.StartsWith("api-key="))
                return line["api-key=".Length..].Trim();
        }

        return null;
    }
}
