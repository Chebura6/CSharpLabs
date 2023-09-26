namespace Isu.Extra.Exceptions;

public class StartLessonTimeException : Exception
{
    private StartLessonTimeException(string message)
        : base(message) { }

    public static StartLessonTimeException InvalidStartLessonTime()
    {
        return new StartLessonTimeException("An attempt to create an object with  an invalid time detected");
    }
}