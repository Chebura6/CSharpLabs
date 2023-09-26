using Banks.Interfaces;
using Spectre.Console;
namespace Banks.Models;

public class SpectreConsoleNotifyer : INotifyer
{
    public void ConfigHasBeenChanged()
    {
        AnsiConsole.Write(new Markup($"Bank config has been [bold green]changed![/]"));
    }
}