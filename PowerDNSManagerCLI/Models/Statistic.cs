using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerDNSManagerCLI.Models;

public class Statistic
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("value")]
    public JsonElement Value { get; set; } // тип может быть int, double, string — используем JsonElement
}
