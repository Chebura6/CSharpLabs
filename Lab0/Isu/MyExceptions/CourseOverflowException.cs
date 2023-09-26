namespace Isu.MyExceptions;

public class CourseOverflowException : Exception
{
    public CourseOverflowException()
        : base("You try using invalid course number") { }
}