using Newtonsoft.Json;

namespace PowerDNSManagerCLI.Models
{
    public class ServerInfo
    {
        public string Id { get; set; } = default!;
        public string DaemonType { get; set; } = default!;
        public string Version { get; set; } = default!;
        public string Url { get; set; } = default!;

    }
}
