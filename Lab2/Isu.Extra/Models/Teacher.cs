namespace Isu.Extra.Models;

public class Teacher : IEquatable<Teacher>
{
    public Teacher(string teacherName)
    {
        if (string.IsNullOrWhiteSpace(teacherName)) throw new ArgumentNullException("Null room number detected");
        int countWords = teacherName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length;
        if (countWords is 2 or 3)
        {
            TeacherName = teacherName;
        }
    }

    internal string TeacherName { get; }

    public bool Equals(Teacher other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TeacherName == other.TeacherName;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Teacher)obj);
    }

    public override int GetHashCode()
    {
        return TeacherName != null ? TeacherName.GetHashCode() : 0;
    }
}