namespace Isu.MyExceptions;

public class NullCourseNumberException : Exception
{
    public NullCourseNumberException()
        : base("Null course number detected") { }
}