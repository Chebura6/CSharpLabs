namespace Banks.Exceptions;

public class BanksConsoleApplicationException : Exception
{
    private BanksConsoleApplicationException(string message)
        : base(message) { }

    public static BanksConsoleApplicationException InvalidBankName()
    {
        return new BanksConsoleApplicationException("Invalid Bank name");
    }

    public static BanksConsoleApplicationException InvalidValue()
    {
        return new BanksConsoleApplicationException("You cant set negative value");
    }

    public static BanksConsoleApplicationException InvalidBound()
    {
        return new BanksConsoleApplicationException("Bound must grow");
    }
}