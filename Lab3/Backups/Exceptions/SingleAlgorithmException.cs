namespace Backups.Exceptions;

public class SingleAlgorithmException : Exception
{
    private SingleAlgorithmException(string message)
        : base(message) { }

    public static SingleAlgorithmException InvalidCountOfObjects()
    {
        return new SingleAlgorithmException("You must use this algo only for some objects");
    }
}