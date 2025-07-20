using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerDNSManagerCLI.Models
{
    public class ZoneInfo
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
        public bool Dnssec { get; set; }
        public int Serial { get; set; }
        public string Kind { get; set; } = default!;
    }
}
