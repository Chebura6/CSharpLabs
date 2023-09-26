namespace Isu.MyExceptions;

public class NotValidStudentIDException : Exception
{
    public NotValidStudentIDException()
        : base("Not valid student ID") { }
}