namespace Isu.MyExceptions;

public class NullGroupNameException : Exception
{
    public NullGroupNameException()
        : base("Null group name") { }
}