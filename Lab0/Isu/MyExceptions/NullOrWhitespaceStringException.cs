namespace Isu.MyExceptions;

public class NullOrWhitespaceStringException : Exception
{
    public NullOrWhitespaceStringException()
        : base("Null or whitespace string detected") { }
}