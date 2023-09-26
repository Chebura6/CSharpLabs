namespace Isu.MyExceptions;

public class StudentIsNotFoundException : Exception
{
    public StudentIsNotFoundException()
        : base("Student is not found") { }
}