using PowerDNSManagerCLI.Models;
using Spectre.Console;

namespace PowerDNSManagerCLI.UI;

internal static class StatisticsView
{
    public static void Show(IEnumerable<Statistic> statistics)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[bold]Название[/]")
            .AddColumn("[bold]Значение[/]");

        foreach (var stat in statistics)
        {
            table.AddRow(stat.Name, stat.Value);
        }

        AnsiConsole.Write(table);
    }
}
