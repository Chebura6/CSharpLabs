namespace Isu.MyExceptions;

public class StudentsLimitException : Exception
{
    public StudentsLimitException()
        : base("You reached students limit in group") { }
}