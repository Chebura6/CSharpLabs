using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Spectre.Console;
namespace Banks.Console.BanksConsoleApplication;

public class StartMenu
{
    public static void Welcome()
    {
        AnsiConsole.Write(
            new FigletText("Welcome to my Banks system!")
                .Centered()
                .Color(Color.Wheat1));

        AnsiConsole.Progress()
            .Start(ctx =>
            {
                ProgressTask task1 = ctx.AddTask("[green]Loading system...[/]");
                while (!ctx.IsFinished)
                {
                    task1.Increment(0.00003);
                }
            });
    }

    public static string OperationsMenu()
    {
        // Ask for the user's favorite fruit
        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What's we'll [green]do[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new[]
                {
                    "Add Bank", "Add Client", "Get config",
                    "Change config", "Make transaction", "Get system state",
                    "Increase time for 1 hour",
                }));
        return option;
    }
}