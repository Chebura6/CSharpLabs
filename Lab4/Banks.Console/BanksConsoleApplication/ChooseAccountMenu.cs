using Banks.Interfaces;
using Banks.Models;
using Spectre.Console;

namespace Banks.Console.BanksConsoleApplication;

public class ChooseAccountMenu
{
    public static IAccount ChooseAccount()
    {
        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which [green]type[/] of account would you want to create?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new[]
                {
                    "Debit", "Credit", "Deposit",
                }));
        return option switch
        {
            "Debit" => new DebitAccount(),
            "Credit" => new CreditAccount(),
            _ => new DepositAccount()
        };
    }
}