using Isu.Models;

namespace Isu.Entities;

public class Student : IEquatable<Student>
{
    private static int _studentsCounter = 0;

    public Student(Group group, string name)
    {
        GroupName = group.GroupName;
        Name = name;
        Course = group.CourseNumber;
        ++_studentsCounter;

        // 999999 is max isu number so to avoid overflow I use %999999
        IsuId = _studentsCounter % 999999;
    }

    public int IsuId { get; }
    public string Name { get; }
    public GroupName GroupName { get; set; }
    public CourseNumber Course { get; }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Student)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Course, IsuId, Name);
    }

    public bool Equals(Student other)
    {
        return other is not null && IsuId == other.IsuId;
    }
}