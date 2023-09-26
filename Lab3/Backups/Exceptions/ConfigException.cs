namespace Backups.Exceptions;

public class ConfigException : Exception
{
    private ConfigException(string message)
        : base(message) { }

    public static ConfigException InvalidObject()
    {
        return new ConfigException("Invalid object");
    }
}