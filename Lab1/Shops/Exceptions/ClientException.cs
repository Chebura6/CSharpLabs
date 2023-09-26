namespace Shops.Exceptions;

public class ClientException : Exception
{
    private ClientException(string message)
        : base(message) { }
    public static ClientException NegativeBalance()
    {
        return new ClientException("Client cannot has negative balance");
    }
}