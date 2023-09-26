using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class TimetableException : Exception
{
    private TimetableException(string message)
        : base(message) { }

    public static TimetableException InvalidCountOfDays()
    {
        return new TimetableException("Invalid count of days");
    }

    public static TimetableException TimetablesAreIntersect()
    {
        return new TimetableException("Timetable intersection detected");
    }
}