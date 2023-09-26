namespace Banks.Exceptions;

public class BankException : Exception
{
    private BankException(string message)
        : base(message) { }

    public static BankException InvalidBankName()
    {
        return new BankException("You cant set empty bank name");
    }
}