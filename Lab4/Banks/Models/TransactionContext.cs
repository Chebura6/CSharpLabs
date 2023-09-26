using Banks.Interfaces;

namespace Banks.Models;

public class TransactionContext
{
    public TransactionContext(IAccount from, IAccount to, decimal amount)
    {
        From = from;
        To = to;
        Amount = amount;
    }

    public IAccount From { get; init; }
    public IAccount To { get; init; }
    public decimal Amount { get; init; }
}