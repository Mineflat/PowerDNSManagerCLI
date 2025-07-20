using PowerDNSManagerCLI.Models;
using PowerDNSManagerCLI.Client;
using Spectre.Console;

namespace PowerDNSManagerCLI.Render;

public static class MenuRenderer
{
    public static void Render(ServerInfo[] servers)
    {
        var server = servers.First(); // пока работаем с первым
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Выберите действие:[/]")
                    .AddChoices("Просмотр зон", "Выход"));

            if (choice == "Выход") break;

            if (choice == "Просмотр зон")
            {
                ShowZones(server);
            }
        }
    }

    private static void ShowZones(ServerInfo server)
    {
        var client = new PowerDnsClient(ConfigHelper.GetApiKey());
        var zones = client.GetZonesAsync(server.Id).GetAwaiter().GetResult();

        var table = new Table()
            .Title($"[blue]Зоны на сервере: {server.Id}[/]")
            .AddColumn("ID")
            .AddColumn("Имя")
            .AddColumn("Тип")
            .AddColumn("DNSSEC")
            .AddColumn("Serial");

        foreach (var zone in zones)
        {
            table.AddRow(
                zone.Id,
                zone.Name,
                zone.Type,
                zone.Dnssec ? "[green]✓[/]" : "[red]✗[/]",
                zone.Serial.ToString());
        }

        AnsiConsole.Write(table);
    }
}
