using PowerDNSManagerCLI.Models;
using Spectre.Console;

namespace PowerDNSManagerCLI.UI;

public static class ServerView
{
    public static void Render(ServerInfo[] servers)
    {
        if (servers.Length == 0)
        {
            AnsiConsole.MarkupLine("[red]Нет доступных серверов для отображения.[/]");
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Expand()
            .Title("[bold blue]Информация о серверах[/]")
            .AddColumn("ID")
            .AddColumn("Тип")
            .AddColumn("Версия")
            .AddColumn("URL");

        foreach (var server in servers)
        {
            table.AddRow(
                $"[green]{server.Id}[/]",
                $"[silver]{server.DaemonType}[/]",
                $"[cyan]{server.Version}[/]",
                $"[grey]{server.Url}[/]"
            );
        }

        AnsiConsole.Write(table);
    }
}
