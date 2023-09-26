using Isu.Models;
using Isu.MyExceptions;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    public const int MaxStudentsInGroup = 35; // max on students in group
    private readonly List<Student> _students;

    public Group(GroupName groupName)
    {
        if (groupName is null) throw new InvalidGroupNameException();
        _students = new List<Student>();
        GroupName = groupName;
        CourseNumber = new CourseNumber(groupName.GetGroupCourse());
    }

    public GroupName GroupName { get; }
    public CourseNumber CourseNumber { get; }

    public IReadOnlyCollection<Student> Students => _students;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Group)obj);
    }

    public override int GetHashCode()
    {
        return GroupName.GetHashCode();
    }

    public bool Equals(Group other)
    {
        return other is not null && GroupName.Equals(other.GroupName);
    }

    internal Student AddStudentInGroup(Student student)
    {
        if (_students.Count == MaxStudentsInGroup) throw new StudentsLimitException();
        _students.Add(student);
        student.GroupName = GroupName;
        return student;
    }

    internal void RemoveStudentFromGroup(Student student)
    {
        if (student is null) throw new NullStudentException();
        _students.Remove(student);
    }
}
