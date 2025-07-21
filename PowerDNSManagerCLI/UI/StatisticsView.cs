using PowerDNSManagerCLI.Models;
using PowerDNSManagerCLI.Services;
using Spectre.Console;

namespace PowerDNSManagerCLI.UI;

public static class StatisticsView
{
    public static void Render(ServerInfo[] servers)
    {
        foreach (var server in servers)
        {
            var statistics = PowerDnsClient.GetStatisticsAsync(server.Id).Result;

            if (statistics.Length == 0)
            {
                AnsiConsole.MarkupLine($"[yellow]Нет статистики для сервера[/] [blue]{server.Id}[/]");
                continue;
            }

            var table = new Table()
                .Border(TableBorder.Rounded)
                .Expand()
                .AddColumn("[green]Имя[/]")
                .AddColumn("[blue]Тип[/]")
                .AddColumn("[magenta]Значение[/]");

            foreach (var stat in statistics)
            {
                table.AddRow(
                    $"[silver]{stat.Name}[/]",
                    $"[grey]{stat.Type}[/]",
                    $"[white]{stat.Value}[/]");
            }

            AnsiConsole.Write(new Panel(table)
                .Header($"📊 Статистика: [bold]{server.Id}[/]")
                .Expand()
                .BorderColor(Color.Green));
        }
    }
}
