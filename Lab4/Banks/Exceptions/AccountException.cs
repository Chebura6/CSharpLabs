namespace Banks.Exceptions;

public class AccountException : Exception
{
    private AccountException(string message)
        : base(message) { }

    public static AccountException LimitExceeded()
    {
        return new AccountException("Transaction limit exceeded");
    }
}