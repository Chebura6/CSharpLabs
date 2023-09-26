using Banks.Exceptions;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console.BanksConsoleApplication;

public class MakeTransaction
{
    public static void Execute(CentralBank centralBank)
    {
        var clientAccountsParser = new ClientAccountsParser();
        clientAccountsParser.Output(centralBank.GetClientAccountsInfo());
        Guid fromID = AnsiConsole.Ask<Guid>("Which account do you want to transfer money [blue]from?[/]");
        Guid toID = AnsiConsole.Ask<Guid>("Which account do you want to transfer money [blue]to?[/]");
        decimal amount = AnsiConsole.Ask<decimal>("What [blue]amount[/] would you like to transfer?");
        if (centralBank.Transfer(fromID, toID, amount)) AnsiConsole.Write(new Markup($"Transaction is [bold green]successful[/]"));
        else AnsiConsole.Write(new Markup($"Transaction is [bold red]failed[/]"));
    }
}