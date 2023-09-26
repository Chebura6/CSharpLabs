namespace Isu.MyExceptions;

public class CourseNumberIsNotValidException : Exception
{
    public CourseNumberIsNotValidException()
        : base("You try use invalid course number") { }
}