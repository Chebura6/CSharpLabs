namespace Isu.Extra.Exceptions;

public class DayOfWeekException : Exception
{
    private DayOfWeekException(string message)
        : base(message) { }
    public static DayOfWeekException TooMuchLessonsInOneDay()
    {
        return new DayOfWeekException("You try set too much lessons");
    }

    public static DayOfWeekException LessonsAreIntersect()
    {
        return new DayOfWeekException("Lessons time intersection detected");
    }
}