using System.Net.Http.Headers;
using System.Text.Json;
using PowerDNSManagerCLI.Models;

namespace PowerDNSManagerCLI.Client;

public class PowerDnsClient
{
    private readonly string _apiKey;
    private readonly HttpClient _http;

    public PowerDnsClient(string apiKey)
    {
        _apiKey = apiKey;
        _http = new HttpClient
        {
            BaseAddress = new Uri("http://127.0.0.1:8081/api/v1/")
        };
        _http.DefaultRequestHeaders.Add("X-API-Key", _apiKey);
    }

    public async Task<ServerInfo[]> GetServersAsync()
    {
        var response = await _http.GetAsync("servers");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ServerInfo[]>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? Array.Empty<ServerInfo>();
    }

    public async Task<ZoneInfo[]> GetZonesAsync(string serverId)
    {
        var response = await _http.GetAsync($"servers/{serverId}/zones");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ZoneInfo[]>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? Array.Empty<ZoneInfo>();
    }
}
