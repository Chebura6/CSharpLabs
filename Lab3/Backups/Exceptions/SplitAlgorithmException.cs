namespace Backups.Exceptions;

public class SplitAlgorithmException : Exception
{
    private SplitAlgorithmException(string message)
        : base(message)
    {
    }

    public static SplitAlgorithmException InvalidCountOfObjects()
    {
        return new SplitAlgorithmException("You must use this algo only for one objects");
    }
}