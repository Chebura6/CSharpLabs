using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class DayOfWeek
{
    public DayOfWeek(List<Lesson> lessons)
    {
        ArgumentNullException.ThrowIfNull(lessons);
        if (lessons.Count > 7)
        {
            DayOfWeekException.TooMuchLessonsInOneDay();
        }

        foreach (var i in lessons)
        {
            var currentLesson = lessons
                .FirstOrDefault(x => x.AreLessonsIntersect(i) && x != i);
            if (currentLesson is not null)
                throw new Exception();
        }

        Lessons = new List<Lesson>();
    }

    internal List<Lesson> Lessons { get; }

    internal void AddLesson(Lesson newLesson)
    {
        if (Lessons.FirstOrDefault(l => l.Time.Equals(newLesson.Time)) is null)
        {
            throw DayOfWeekException.LessonsAreIntersect();
        }

        Lessons.Add(newLesson);
    }

    internal bool AreDaysTimetableIntersect(DayOfWeek otherDay)
    {
        ArgumentNullException.ThrowIfNull(otherDay);
        foreach (var i in otherDay.Lessons)
        {
            var currentLesson = Lessons
                .FirstOrDefault(x => x.AreLessonsIntersect(i));
            if (currentLesson is not null)
            {
                return true;
            }
        }

        return false;
    }
}