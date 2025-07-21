using PowerDNSManagerCLI.Models;
using Spectre.Console;

namespace PowerDNSManagerCLI.UI;

public static class MenuRenderer
{
    public static void Render(ServerInfo[] servers)
    {
        while (true)
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Выберите действие[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "📊 Статистика",
                        "⚙️ Конфигурация",
                        "🌐 Зоны",
                        "💻 Информация о серверах",
                        "🚪 Выход"
                    }));

            switch (option)
            {
                case "📊 Статистика":
                    StatisticsView.Render(servers);
                    break;

                case "⚙️ Конфигурация":
                    ConfigView.Render(servers);
                    break;

                case "🌐 Зоны":
                    ZoneView.Render(servers);
                    break;

                case "💻 Информация о серверах":
                    ServerView.Render(servers);
                    break;

                case "🚪 Выход":
                    return;
            }

            AnsiConsole.MarkupLine("\n[grey]Нажмите [bold]Enter[/] для возврата в меню...[/]");
            Console.ReadLine();
        }
    }
}
