using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class ExtraStudent : IEquatable<ExtraStudent>
{
    public ExtraStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student, "Null student detected");
        Student = student;
    }

    internal OgnpGroupName OgnpGroupName { get; set; }
    internal Student Student { get; }

    public bool Equals(ExtraStudent other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(Student, other.Student);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ExtraStudent)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(OgnpGroupName, Student);
    }
}