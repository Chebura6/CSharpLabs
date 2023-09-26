namespace Isu.MyExceptions;

public class NullStudentException : Exception
{
    public NullStudentException()
        : base("Null student") { }
}