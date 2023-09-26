namespace Banks.Exceptions;

public class ConfigException : Exception
{
    private ConfigException(string message)
        : base(message) { }

    public static ConfigException InvalidArgumentsInConstructor()
    {
        return new ConfigException("Check lists lenght");
    }

    public static ConfigException NegativePercent()
    {
        return new ConfigException("Percent cannot be negative");
    }

    public static ConfigException NegativeLimit()
    {
        return new ConfigException("Transaction limit cannot be negative");
    }
}