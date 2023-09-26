using Isu.Extra.Exceptions;
using DayOfWeek = Isu.Extra.Models.DayOfWeek;

namespace Isu.Extra.Entities;

public class Timetable
{
    private const uint TimetableSize = 14;
    public Timetable(List<DayOfWeek> timetable)
    {
        ArgumentNullException.ThrowIfNull("Null timetable list detected");
        if (timetable.Count != TimetableSize)
        {
            throw TimetableException.InvalidCountOfDays();
        }

        Schedule = timetable;
    }

    internal List<DayOfWeek> Schedule { get; }
    public bool AreTimetablesIntersect(Timetable otherTimetable)
    {
        ArgumentNullException.ThrowIfNull(otherTimetable, "Null timetable detected");
        for (int i = 0; i < TimetableSize; ++i)
        {
            if (Schedule[i].AreDaysTimetableIntersect(otherTimetable.Schedule[i]))
            {
                return true;
            }
        }

        return false;
    }
}