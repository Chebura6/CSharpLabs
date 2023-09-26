using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class Bank : IEquatable<Bank>
{
    public Bank(string name, Config config)
    {
        if (string.IsNullOrEmpty(name)) throw CentralBankException.InvalidBankName();
        ArgumentNullException.ThrowIfNull(config);
        Name = name;
        Config = config;
        Accounts = new List<ClientAccountMap>();
    }

    public string Name { get; }
    internal Config Config { get; set; }
    internal List<ClientAccountMap> Accounts { get; set; }
    public void CalculateThePercentageOnTheBalance()
    {
        foreach (ClientAccountMap account in Accounts)
        {
            account.Account.CalculatePercentages();
        }
    }

    public bool Equals(Bank other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Bank)obj);
    }

    public override int GetHashCode()
    {
        return Name != null ? Name.GetHashCode() : 0;
    }

    public override string ToString()
    {
        string totalString = string.Empty;
        foreach (ClientAccountMap clientAccountMap in Accounts)
        {
            totalString = string.Concat(totalString, $"\t{Name}:\n");
            totalString = string.Concat(totalString, clientAccountMap.Client.ToString(), clientAccountMap.Account.ToString());
        }

        return totalString;
    }

    internal void AddAccount(Client client, IAccount account)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(account);
        account.FillWithData(Config);
        Accounts.Add(new ClientAccountMap(client, account));
    }

    internal void ChargeInterests()
    {
        foreach (ClientAccountMap account in Accounts)
        {
            account.Account.ChargeInterests();
        }
    }

    internal void SetAccountsTime(DateTime dateTime)
    {
        foreach (ClientAccountMap clientAccountMap in Accounts)
        {
            if (clientAccountMap.Account is DepositAccount)
            {
                clientAccountMap.Account.SetTime(dateTime);
            }
        }
    }

    internal void RemoveAccountsFromFuture(DateTime dateTime)
    {
        var newAccounts = new List<ClientAccountMap>(Accounts);
        foreach (ClientAccountMap clientAccountMap in Accounts)
        {
            if (clientAccountMap.Account.AccountCreationTime > dateTime)
            {
                newAccounts.Remove(clientAccountMap);
            }
        }

        Accounts = newAccounts;
    }
}
