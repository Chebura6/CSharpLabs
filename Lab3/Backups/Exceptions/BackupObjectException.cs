namespace Backups.Exceptions;

public class BackupObjectException : Exception
{
    private BackupObjectException(string message)
        : base(message) { }

    public static BackupObjectException InvalidPath()
    {
        return new BackupObjectException("Invalid path");
    }
}