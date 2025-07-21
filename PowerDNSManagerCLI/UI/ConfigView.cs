using PowerDNSManagerCLI.Services;
using Spectre.Console;

namespace PowerDNSManagerCLI.UI;

public static class ConfigView
{
    public static void Render()
    {
        var configEntries = ConfigParser.GetAllConfig().ToList();

        if (!configEntries.Any())
        {
            AnsiConsole.MarkupLine("[yellow]Конфигурационный файл пуст или отсутствует.[/]");
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Expand()
            .AddColumn("[green]Ключ[/]")
            .AddColumn("[blue]Значение[/]");

        foreach (var entry in configEntries)
        {
            table.AddRow($"[silver]{entry.Key}[/]", $"[grey]{entry.Value}[/]");
        }

        AnsiConsole.Write(new Panel(table)
            .Header("Конфигурация PowerDNS")
            .Expand()
            .BorderColor(Color.Blue3_1));
    }
}
