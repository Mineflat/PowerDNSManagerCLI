using System.Text.Json.Serialization;

namespace PowerDNSManagerCLI.Models;

public class ConfigEntry
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("value")]
    public string? Value { get; set; }

    [JsonPropertyName("default")]
    public string? Default { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
