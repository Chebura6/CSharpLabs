using Isu.Extra.Exceptions;
using Isu.MyExceptions;

namespace Isu.Extra.Models;

public class OgnpGroupName
{
    public OgnpGroupName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new InvalidGroupNameException();
        if (!char.IsLetter(name[0]))
        {
            throw OgnpGroupNameException.InvalidOgnpGroupName();
        }

        if (name.Length is < 5 or > 6) throw OgnpGroupNameException.InvalidOgnpGroupName();
        Name = name;
    }

    internal string Name { get; }
}