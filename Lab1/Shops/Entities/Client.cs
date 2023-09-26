using Shops.Exceptions;

namespace Shops.Entities;

public class Client
{
    public Client(decimal balance)
    {
        if (balance < 0) throw ClientException.NegativeBalance();
        Balance = balance;
    }

    public decimal Balance { get; private set; }

    public void CreatePurchase(decimal purchaseCost)
    {
        Balance -= purchaseCost;
    }
}