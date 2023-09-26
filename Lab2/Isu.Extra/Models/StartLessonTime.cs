using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class StartLessonTime : IEquatable<StartLessonTime>
{
    public StartLessonTime(string time)
    {
        if (time is "8:20" or "10:00" or "11:40" or "13:30" or "15:20" or "17:00" or "18:40")
        {
            Time = time;
        }
        else
        {
            throw StartLessonTimeException.InvalidStartLessonTime();
        }
    }

    internal string Time { get; }

    public bool Equals(StartLessonTime other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Time == other.Time;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((StartLessonTime)obj);
    }

    public override int GetHashCode()
    {
        return Time != null ? Time.GetHashCode() : 0;
    }
}