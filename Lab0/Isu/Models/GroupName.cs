using Isu.MyExceptions;

namespace Isu.Models;

public class GroupName : IEquatable<GroupName>
{
    private const int AsciiShift = 48;
    private const int GroupCoursePositionInGroupName = 2;
    public GroupName(string name)
    {
        if (name is null)
        {
            throw new InvalidGroupNameException();
        }

        if (string.IsNullOrWhiteSpace(name)) throw new InvalidGroupNameException();

        if (name[0] != 'M')
        {
            if (name[0] == 'М')
            {
                throw new InvalidGroupNameException();
            }

            throw new InvalidGroupNameException();
        }

        if (name.Length is < 5 or > 6) throw new InvalidGroupNameException();

        Name = name;
    }

    public string Name { get; }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((GroupName)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public int GetGroupCourse()
    {
        return Name[GroupCoursePositionInGroupName] - AsciiShift;
    }

    public bool Equals(GroupName other)
    {
        return other is not null && Name == other.Name;
    }
}