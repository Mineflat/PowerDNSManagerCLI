using PowerDNSManagerCLI.Models;
using PowerDNSManagerCLI.Render;
using PowerDNSManagerCLI.UI;
using Spectre.Console;

namespace PowerDNSManagerCLI
{
    internal class Program
    {
        static async Task Main()
        {
            var apiKey = ConfigParser.GetApiKey();
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                AnsiConsole.MarkupLine("[red]API-ключ не найден в pdns.conf[/]");
                return;
            }

            var client = new PowerDnsClient(apiKey);

            var servers = await client.GetServersAsync();
            if (servers is null || servers.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Серверы не найдены[/]");
                return;
            }

            var serverId = MenuRenderer.SelectServer(servers);
            if (string.IsNullOrWhiteSpace(serverId)) return;

            while (true)
            {
                var action = MenuRenderer.ShowMainMenu(serverId);
                switch (action)
                {
                    case "Конфигурация":
                        var config = await client.GetConfigAsync(serverId);
                        MenuRenderer.RenderConfig(config);
                        break;

                    case "Статистика":
                        var stats = await client.GetStatisticsAsync(serverId);
                        MenuRenderer.RenderStatistics(stats);
                        break;

                    case "Зоны":
                        var zones = await client.GetZonesAsync(serverId);
                        MenuRenderer.RenderZones(zones);
                        break;

                    case "Выход":
                        return;
                }
            }
        }

        static async Task ShowServers(PowerDnsClient client)
        {
            var servers = await client.GetServersAsync();
            var table = new Table().Border(TableBorder.Rounded).Centered();
            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[green]Тип[/]");
            table.AddColumn("[blue]Версия[/]");

            foreach (var srv in servers)
                table.AddRow(srv.Id, srv.DaemonType, srv.Version);

            AnsiConsole.Write(table);
        }

        static async Task ShowConfig(PowerDnsClient client)
        {
            var config = await client.GetConfigAsync();
            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("[green]Параметр[/]");
            table.AddColumn("[white]Значение[/]");

            foreach (var pair in config)
                table.AddRow(pair.Key, pair.Value);

            AnsiConsole.Write(table);
        }

        static async Task ShowStatistics(PowerDnsClient client)
        {
            var stats = await client.GetStatisticsAsync();
            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("[cyan]Метрика[/]");
            table.AddColumn("[white]Значение[/]");

            foreach (var pair in stats)
                table.AddRow(pair.Name, pair.Value);

            AnsiConsole.Write(table);
        }

        static async Task ShowZones(PowerDnsClient client)
        {
            var zones = await client.GetZonesAsync();
            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("[green]Зона[/]");
            table.AddColumn("[yellow]Kind[/]");
            table.AddColumn("[blue]Serial[/]");

            foreach (var zone in zones)
                table.AddRow(zone.Name, zone.Kind, zone.Serial.ToString());

            AnsiConsole.Write(table);
        }

        static async Task ViewZone(PowerDnsClient client)
        {
            var name = AnsiConsole.Ask<string>("Введите имя зоны (с точкой):");
            var zone = await client.GetZoneAsync(name);
            AnsiConsole.MarkupLine("[bold yellow]Зона:[/] [green]{0}[/]", zone.Name);

            var table = new Table().Border(TableBorder.Minimal);
            table.AddColumn("[blue]Тип[/]");
            table.AddColumn("[white]Имя[/]");
            table.AddColumn("[grey]Данные[/]");

            foreach (var record in zone.Records)
                table.AddRow(record.Type, record.Name, string.Join(", ", record.Records));

            AnsiConsole.Write(table);
        }
    }
}
