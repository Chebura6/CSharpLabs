namespace Isu.MyExceptions;

public class InvalidGroupNameException : Exception
{
    public InvalidGroupNameException()
        : base("You try using invalid group name") { }
}