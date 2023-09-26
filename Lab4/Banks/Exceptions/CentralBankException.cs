namespace Banks.Exceptions;

public class CentralBankException : Exception
{
    private CentralBankException(string message)
        : base(message) { }

    public static CentralBankException InvalidComission()
    {
        return new CentralBankException("You cant set negative comission");
    }

    public static CentralBankException InvalidBankName()
    {
        return new CentralBankException("You cant set empty bank name");
    }

    public static CentralBankException InvalidDebitPercent()
    {
        return new CentralBankException("You cant set negative percent");
    }

    public static CentralBankException InvalidTransactionRestrictionLimit()
    {
        return new CentralBankException("You cant set empty bank name");
    }
}