using System.Text.Json.Serialization;

namespace PowerDNSManagerCLI.Models;

public class ServerInfo
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;

    [JsonPropertyName("daemon_type")]
    public string DaemonType { get; set; } = default!;

    [JsonPropertyName("version")]
    public string Version { get; set; } = default!;

    [JsonPropertyName("config_url")]
    public string ConfigUrl { get; set; } = default!;

    [JsonPropertyName("zones_url")]
    public string ZonesUrl { get; set; } = default!;

    [JsonPropertyName("autoprimaries_url")]
    public string AutoPrimariesUrl { get; set; } = default!;
}
