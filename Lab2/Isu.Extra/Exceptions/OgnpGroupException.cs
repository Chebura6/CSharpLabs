namespace Isu.Extra.Exceptions;

public class OgnpGroupException : Exception
{
    private OgnpGroupException(string message)
        : base(message) { }
    public static OgnpGroupException GroupOverload()
    {
        return new OgnpGroupException("Too much students in one ognp group");
    }

    public static OgnpGroupException GroupNotFound()
    {
        return new OgnpGroupException("Group not found");
    }
}