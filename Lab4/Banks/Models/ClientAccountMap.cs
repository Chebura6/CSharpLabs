using Banks.Interfaces;

namespace Banks.Models;

public class ClientAccountMap
{
    public ClientAccountMap(Client client, IAccount account)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(account);
        Client = client;
        Account = account;
    }

    internal Client Client { get; }
    internal IAccount Account { get; }

    public override string ToString()
    {
        return string.Concat(Client.ToString(), Account.ToString());
    }
}