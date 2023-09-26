using Isu.MyExceptions;

namespace Isu.Models;

public class CourseNumber : IEquatable<CourseNumber>
{
    public const int MinPossibleCourse = 1;
    public const int MaxPossibleCourse = 4;
    public CourseNumber(int number)
    {
        if (number is < MinPossibleCourse or > MaxPossibleCourse)
        {
            throw new CourseNumberIsNotValidException();
        }

        Number = number;
    }

    public int Number { get; }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CourseNumber)obj);
    }

    public override int GetHashCode()
    {
        return Number;
    }

    public bool Equals(CourseNumber other)
    {
        return Number == other.Number;
    }
}