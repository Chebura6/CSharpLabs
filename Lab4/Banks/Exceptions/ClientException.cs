namespace Banks.Exceptions;

public class ClientException : Exception
{
    private ClientException(string message)
        : base(message) { }

    public static ClientException InvalidParameter()
    {
        return new ClientException("You cannot set empty string");
    }
}