using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.BanksConsoleApplication;

public class FillBankParams
{
    private const long CountOfBounds = 3;
    private const long DateTimeParametersCount = 4;
    public static string GetBankName()
    {
        string name = AnsiConsole.Ask<string>("What's bank [green]name[/]?");
        if (string.IsNullOrWhiteSpace(name)) throw BanksConsoleApplicationException.InvalidBankName();
        return name;
    }

    public static decimal GetDebitPercent()
    {
        decimal percent = AnsiConsole.Ask<decimal>("What's [green]debit percent[/]?");
        if (percent < 0) throw BanksConsoleApplicationException.InvalidValue();
        return percent;
    }

    public static List<decimal> GetLowerBounds()
    {
        AnsiConsole.Write(new Markup($"What's [green]lower bounds[/]? You need to enter [bold green]{CountOfBounds}[/] numbers."));
        decimal bound = -1;
        decimal maxBound = bound;
        var bounds = new List<decimal>();
        for (int i = 0; i < CountOfBounds; i++)
        {
            bound = AnsiConsole.Ask<decimal>($"Input [green]{i}[/] number");
            if (bound > maxBound) maxBound = bound;
            else throw BanksConsoleApplicationException.InvalidBound();
            if (bound < 0) throw BanksConsoleApplicationException.InvalidValue();
            bounds.Add(bound);
        }

        return bounds;
    }

    public static List<decimal> GetPercents()
    {
        AnsiConsole.Write(new Markup($"What's [green]percents[/]? You need to enter [bold green]{CountOfBounds}[/] numbers."));
        decimal percent = 0;
        var percents = new List<decimal>();
        for (int i = 0; i < CountOfBounds; i++)
        {
            percent = AnsiConsole.Ask<decimal>($"Input [green]{i + 1}[/] number");
            if (percent < 0) throw BanksConsoleApplicationException.InvalidValue();
            percents.Add(percent);
        }

        return percents;
    }

    public static decimal GetCreditCommission()
    {
        decimal commission = AnsiConsole.Ask<decimal>("What's [green]credit percents[/]?");
        if (commission < 0) throw BanksConsoleApplicationException.InvalidValue();
        return commission;
    }

    public static TimeSpan GetRestrictionDuration()
    {
        var dateTimeFormat = new List<string> { "days", "hours", "minutes", "seconds" };
        var list = new List<int>();
        for (int i = 0; i < DateTimeParametersCount; i++)
        {
           int time = AnsiConsole.Ask<int>($"Input restriction duration {dateTimeFormat[i]}:");
           if (time < 0) throw BanksConsoleApplicationException.InvalidValue();
           list.Add(time);
        }

        return TimeSpanFormatParse(list);
    }

    public static decimal GetTransactionRestrictionLimit()
    {
        decimal limit = AnsiConsole.Ask<decimal>("What's transaction restriction [green]limit[/]?");
        if (limit < 0) throw BanksConsoleApplicationException.InvalidValue();
        return limit;
    }

    public static decimal GetCommissionForTransactions()
    {
        decimal commission = AnsiConsole.Ask<decimal>("What's [green]commission for transactions[/]?");
        if (commission < 0) throw BanksConsoleApplicationException.InvalidValue();
        return commission;
    }

    private static TimeSpan TimeSpanFormatParse(List<int> input)
    {
        return new TimeSpan(input[0], input[1], input[2], input[3]);
    }
}