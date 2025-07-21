using System.Text.Json.Serialization;

namespace PowerDNSManagerCLI.Models;

public class ZoneInfo
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("kind")]
    public string Kind { get; set; } = default!;

    [JsonPropertyName("dnssec")]
    public bool Dnssec { get; set; }

    [JsonPropertyName("serial")]
    public long Serial { get; set; }

    [JsonPropertyName("notified_serial")]
    public long? NotifiedSerial { get; set; }

    [JsonPropertyName("masters")]
    public List<string> Masters { get; set; } = [];

    [JsonPropertyName("nameservers")]
    public List<string> Nameservers { get; set; } = [];
}
