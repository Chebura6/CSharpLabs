namespace Backups.Exceptions;

public class BackupTaskException : Exception
{
    private BackupTaskException(string message)
        : base(message) { }

    public static BackupTaskException InvalidPath()
    {
        return new BackupTaskException("Invalid path");
    }

    public static BackupTaskException CountOfObjectsException()
    {
        return new BackupTaskException("You cant execute backup task without objects");
    }
}