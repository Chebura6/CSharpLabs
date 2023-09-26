namespace Backups.Exceptions;

public class RepositoryException : Exception
{
    private RepositoryException(string message)
        : base(message) { }

    public static RepositoryException InvalidPath()
    {
        return new RepositoryException("Invalid path");
    }
}