using Spectre.Console;
using PowerDNSManagerCLI.Models;
using PowerDNSManagerCLI.UI;

namespace PowerDNSManagerCLI.UI;

public static class MenuRenderer
{
    public static void Render(ServerInfo[] servers)
    {
        while (true)
        {
            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Выберите действие[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Просмотр конфигурации",
                        "Просмотр серверов",
                        "Просмотр статистики",
                        "Просмотр зон",
                        "[red]Выход[/]"
                    }));

            switch (selected)
            {
                case "Просмотр конфигурации":
                    ConfigView.Render();
                    break;

                case "Просмотр серверов":
                    ServerView.Render(servers);
                    break;

                case "Просмотр статистики":
                    StatisticsView.Render(servers);
                    break;

                case "Просмотр зон":
                    ZoneView.Render(servers);
                    break;

                case "[red]Выход[/]":
                    return;
            }

            AnsiConsole.MarkupLine("\n[grey]Нажмите любую клавишу для возврата в меню...[/]");
            Console.ReadKey(true);
        }
    }
}
