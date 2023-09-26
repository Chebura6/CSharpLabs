namespace Isu.Extra.Models;

public class Lesson : IEquatable<Lesson>
{
    public Lesson(string lessonName, StartLessonTime time, Teacher teacher, Room room)
    {
        if (string.IsNullOrWhiteSpace(lessonName)) throw new ArgumentNullException("Null lesson name detected");
        ArgumentNullException.ThrowIfNull(time, "Null time detected");
        ArgumentNullException.ThrowIfNull(teacher, "Null teacher detected");
        ArgumentNullException.ThrowIfNull(room);
        LessonName = lessonName;
        Time = time;
        Teacher = teacher;
    }

    internal string LessonName { get; }
    internal StartLessonTime Time { get; }
    internal Teacher Teacher { get; }
    internal Room Room { get; }

    public bool Equals(Lesson other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return LessonName == other.LessonName && Equals(Time, other.Time) && Equals(Teacher, other.Teacher) && Equals(Room, other.Room);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Lesson)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(LessonName, Time, Teacher, Room);
    }

    internal bool AreLessonsIntersect(Lesson otherLesson)
    {
        ArgumentNullException.ThrowIfNull(otherLesson, "Lesson is null");
        return Time.Equals(otherLesson.Time);
    }
}