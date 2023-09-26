using Spectre.Console;

namespace Banks.Console.BanksConsoleApplication;

public class ClientAccountsParser
{
    public void Output(string[] inputString)
    {
        var table = new Table();
        table.AddColumn("Client");

        table.AddColumn("Account");
        for (int i = 0; i < inputString.Length; i += 10)
        {
            table.AddRow(new Panel(string.Concat(inputString[i], inputString[i + 1], inputString[i + 2], inputString[i + 3], inputString[i + 4])), new Panel(string.Concat(inputString[i + 5], inputString[i + 6], inputString[i + 7], inputString[i + 8], inputString[i + 9])));
        }

        AnsiConsole.Write(table);
    }
}