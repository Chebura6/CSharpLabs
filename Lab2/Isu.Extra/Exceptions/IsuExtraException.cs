namespace Isu.Extra.Exceptions;

public class IsuExtraException : Exception
{
    private IsuExtraException(string message)
        : base(message) { }
    public static IsuExtraException ImpossibleAddStudentInOgnpGroup()
    {
        return new IsuExtraException("We cant add this student in ognp group student");
    }

    public static IsuExtraException GroupNotFound()
    {
        return new IsuExtraException("Group not found");
    }
}