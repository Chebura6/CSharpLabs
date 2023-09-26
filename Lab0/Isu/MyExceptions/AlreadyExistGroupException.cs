namespace Isu.MyExceptions;

public class AlreadyExistGroupException : Exception
{
    public AlreadyExistGroupException()
        : base("Group with the same name already exists") { }
}