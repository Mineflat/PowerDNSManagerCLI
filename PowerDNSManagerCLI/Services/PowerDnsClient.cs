using System.Net.Http.Json;
using System.Text.Json;
using PowerDNSManagerCLI.Models;

namespace PowerDNSManagerCLI.Services;

public class PowerDnsClient
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _jsonOptions;

    public PowerDnsClient(string apiKey)
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri("http://127.0.0.1:8081/api/v1/servers/")
        };
        _http.DefaultRequestHeaders.Add("X-API-Key", apiKey);

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };
    }

    public async Task<ServerInfo[]> GetServersAsync()
    {
        var response = await _http.GetAsync("");
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<ServerInfo[]>(stream, _jsonOptions) ?? Array.Empty<ServerInfo>();
    }

    public async Task<List<ConfigEntry>> GetConfigAsync(string serverId)
    {
        var response = await _http.GetAsync($"{serverId}/config");
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<List<ConfigEntry>>(stream, _jsonOptions) ?? new();
    }

    public async Task<List<Statistic>> GetStatisticsAsync(string serverId)
    {
        var response = await _http.GetAsync($"{serverId}/statistics");
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<List<Statistic>>(stream, _jsonOptions) ?? new();
    }

    public async Task<List<ZoneInfo>> GetZonesAsync(string serverId)
    {
        var response = await _http.GetAsync($"{serverId}/zones");
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<List<ZoneInfo>>(stream, _jsonOptions) ?? new();
    }
}
