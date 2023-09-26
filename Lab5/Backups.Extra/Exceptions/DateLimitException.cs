namespace Backups.Extra.Exceptions;

public class DateLimitException : Exception
{
    private DateLimitException(string message)
        : base(message) { }

    public static DateLimitException InvalidPointsLimit()
    {
        return new DateLimitException("You limit parameter is too strict");
    }
}