namespace Isu.MyExceptions;

public class InvalidCourseNumberException : Exception
{
    public InvalidCourseNumberException()
        : base("Try change course number") { }
}