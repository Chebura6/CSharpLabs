namespace Isu.Extra.Exceptions;

public class OgnpGroupNameException : Exception
{
    private OgnpGroupNameException(string message)
        : base(message) { }
    public static OgnpGroupNameException InvalidOgnpGroupName()
    {
        return new OgnpGroupNameException("Detected attempt to set invalid Ognp group name");
    }
}