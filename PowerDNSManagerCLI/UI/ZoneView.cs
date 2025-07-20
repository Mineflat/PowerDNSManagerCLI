using PowerDNSManagerCLI.Models;
using Spectre.Console;

namespace PowerDNSManagerCLI.UI;

internal static class ZoneView
{
    public static void Show(IEnumerable<ZoneInfo> zones)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[bold]Имя зоны[/]")
            .AddColumn("[bold]Тип[/]")
            .AddColumn("[bold]Serial[/]")
            .AddColumn("[bold]Kind[/]");

        foreach (var zone in zones)
        {
            table.AddRow(zone.Name, zone.Type, zone.Serial.ToString(), zone.Kind ?? "-");
        }

        AnsiConsole.Write(table);
    }
}
