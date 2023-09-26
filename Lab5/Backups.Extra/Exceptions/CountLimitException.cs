namespace Backups.Extra.Exceptions;

public class CountLimitException : Exception
{
    private CountLimitException(string message)
        : base(message) { }

    public static CountLimitException InvalidPointsLimit()
    {
        return new CountLimitException("Limit cant be less than 1");
    }

    public static CountLimitException InvalidLimitParamter()
    {
        return new CountLimitException("You limit parameter is too strict");
    }
}